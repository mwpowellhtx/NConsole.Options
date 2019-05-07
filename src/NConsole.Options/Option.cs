using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NConsole.Options
{
    using static Characters;
    using static Domain;
    using static String;

    public abstract class Option
    {
        public string Prototype { get; }

        public string Description { get; }

        public OptionValueType ValueType { get; }

        public int MaximumValueCount { get; }

        internal string[] Names { get; }

        internal string[] ValueSeparators { get; private set; }

        /// <summary>
        /// <see cref="char"/> array defaults to <see cref="Equal"/> and <see cref="Colon"/>.
        /// </summary>
        private char[] NameTerminator { get; } = {Equal, Colon};

        protected Option(string prototype, string description)
            : this(prototype, description, 1)
        {
        }

        protected Option(string prototype, string description, int maximumValueCount)
        {
            if (prototype == null)
            {
                throw new ArgumentNullException(nameof(prototype));
            }

            if (!prototype.Any())
            {
                throw new ArgumentException("Cannot be the empty string.", nameof(prototype));
            }

            if (maximumValueCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumValueCount));
            }

            Names = prototype.Split(Pipe);
            Description = description;
            MaximumValueCount = maximumValueCount;
            ValueType = ParsePrototype(Prototype = prototype);

            ArgumentException ThrowMaximumValueCount(params OptionValueType[] traits)
            {
                var traitPrefix = $"{nameof(OptionValueType)}";

                string EnumeratedTraits() => Join($" {or} ", traits.Select(x => $"`{traitPrefix}{Dot}{x}'"));

                return new ArgumentException(
                    $"Cannot provide `{nameof(maximumValueCount)}' of {maximumValueCount} for {EnumeratedTraits()}."
                    , nameof(maximumValueCount));
            }

            if (MaximumValueCount == 0 && ValueType != OptionValueType.None)
            {
                throw ThrowMaximumValueCount( OptionValueType.Required, OptionValueType.Optional);
            }

            if (ValueType == OptionValueType.None && maximumValueCount > 1)
            {
                throw ThrowMaximumValueCount(OptionValueType.None);
            }

            if (Array.IndexOf(Names, AngleBrackets) >= 0
                && ((Names.Length == 1 && ValueType != OptionValueType.None)
                    || (Names.Length > 1 && MaximumValueCount > 1)))
            {
                throw new ArgumentException(
                    $"The default option handler '{AngleBrackets}' cannot require values."
                    , nameof(prototype));
            }
        }

        // TODO: TBD: Seriously? This works? What are they here for? Testing? Would subscribers be using these?
        public string[] GetNames() => (string[]) Names.Clone();

        public string[] GetValueSeparators() => ValueSeparators == null ? new string[] { } : (string[]) ValueSeparators.Clone();

        protected static T Parse<T>(string value, OptionContext context)
        {
            T GetDefaultValue(out Type x)
            {
                var candidateType = typeof(T);

                bool IsNullable()
                    => candidateType.IsValueType
                       && candidateType.IsGenericType
                       && !candidateType.IsGenericTypeDefinition
                       && candidateType.GetGenericTypeDefinition() == typeof(Nullable<>);

                x = IsNullable() ? candidateType.GetGenericArguments()[0] : candidateType;
                return default;
            }

            var defaultValue = GetDefaultValue(out var targetType);
            var typeConverter = TypeDescriptor.GetConverter(targetType);

            try
            {
                if (value != null)
                {
                    defaultValue = (T) typeConverter.ConvertFromString(value);
                }
            }
            catch (Exception ex)
            {
                throw new OptionException(
                    Format(
                        context.OptionSet.MessageLocalizer("Could not convert string `{0}' to type {1} for option `{2}'."),
                        value, targetType.Name, context.OptionName),
                    context.OptionName, ex);
            }

            return defaultValue;
        }

        private OptionValueType ParsePrototype(string prototype)
        {
            char? parsedType = null;
            var separators = new List<string>();

            OptionValueType ReturnParsedType() => parsedType == Equal ? OptionValueType.Required : OptionValueType.Optional;

            for (var i = 0; i < Names.Length; ++i)
            {
                var name = Names[i];
                if (name.Length == 0)
                {
                    throw new ArgumentException("Empty option names are not supported.", nameof(prototype));
                }

                var end = name.IndexOfAny(NameTerminator);
                if (end == -1)
                {
                    continue;
                }

                Names[i] = name.Substring(0, end);
                if (!parsedType.HasValue || parsedType == name[end])
                {
                    parsedType = name[end];
                }
                else
                {
                    throw new ArgumentException($"Conflicting option types: '{parsedType}' vs. '{name[end]}'."
                        , nameof(prototype));
                }

                AddSeparators(prototype, name, end, separators);
            }

            if (!parsedType.HasValue)
            {
                return OptionValueType.None;
            }

            if (MaximumValueCount <= 1 && separators.Count != 0)
            {
                throw new ArgumentException(
                    "Cannot provide key/value separators for Options taking"
                    + $" {MaximumValueCount} value{(MaximumValueCount == 1 ? "" : "s")}."
                    , nameof(prototype));
            }

            if (MaximumValueCount <= 1)
            {
                return ReturnParsedType();
            }

            switch (separators.Count)
            {
                case 0:
                    ValueSeparators = new[] { $"{Colon}", $"{Equal}" };
                    break;

                case 1 when !IsNullOrEmpty(separators[0]):
                    ValueSeparators = null;
                    break;

                default:
                    ValueSeparators = separators.ToArray();
                    break;
            }

            return ReturnParsedType();
        }

        // ReSharper disable once UnusedParameter.Local
        private void AddSeparators(string prototype, string name, int end, ICollection<string> separators)
        {
            var start = -1;

            for (var i = end + 1; i < name.Length; ++i)
            {
                switch (name[i])
                {
                    case CurlyBracesOpen:

                        if (start != -1)
                        {
                            throw new ArgumentException($"Ill-formed name/value separator found in `{name}'."
                                , nameof(prototype));
                        }

                        start = i + 1;
                        break;

                    case CurlyBracesClose:

                        if (start == -1)
                        {
                            throw new ArgumentException($"Ill-formed name/value separator found in `{name}'."
                                , nameof(prototype));
                        }

                        separators.Add(name.Substring(start, i - start));
                        start = -1;
                        break;

                    default:

                        if (start == -1)
                        {
                            separators.Add($"{name[i]}");
                        }

                        break;
                }
            }

            if (start != -1)
            {
                throw new ArgumentException($"Ill-formed name/value separator found in `{name}'.", nameof(prototype));
            }
        }

        public void Invoke(OptionContext context)
        {
            OnParsed(context);
            context.OptionName = null;
            context.Option = null;
            context.OptionValues.Clear();
        }

        protected abstract void OnParsed(OptionContext context);

        public override string ToString() => Prototype;
    }
}