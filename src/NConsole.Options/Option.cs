using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace NConsole.Options
{
    using static Characters;
    using static Domain;
    using static Option.RegexGroupNames;
    using static String;
    using static OptionValueType;
    using static RegexOptions;
    using static StringSplitOptions;

    /// <summary>
    /// Represents an Option asset concern.
    /// </summary>
    /// <inheritdoc />
    public abstract class Option : IOption
    {
        internal Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public string Prototype { get; }

        /// <inheritdoc />
        public string Description { get; }

        private readonly OptionValueType? _valueType;

        /// <inheritdoc />
        public OptionValueType? ValueType => _valueType;

        // TODO: TBD: "maximum value count" ? does this really serve any real purpose given the dispatch strategies?
        // TODO: TBD: ditto the original assumptions... I think it has more to do with the functional spec, "simple", "target", "key/value" and so on.
        private int _maximumValueCount;

        // ReSharper disable once StringLiteralTypo
        /// <summary>
        /// Gets the MaximumValueCount.
        /// </summary>
        [Obsolete("We are thinking this is potentially a non-sequitur, that the functional specification should drive the `maximum value count'.")]
        public int MaximumValueCount
        {
            get => _maximumValueCount;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                ArgumentException ThrowMaximumValueCount(params OptionValueType[] traits)
                {
                    var traitPrefix = $"{nameof(OptionValueType)}";

                    string EnumeratedTraits() => traits.Any()
                        ? Join($" {or} ", traits.Select(x => $"`{traitPrefix}{Dot}{x}'"))
                        : "[No Traits Specified]";

                    return new ArgumentException(
                        $"Cannot provide `{nameof(value)}' of {value} for {EnumeratedTraits()}."
                        , nameof(value));
                }

                switch (value)
                {
                    case 0 when ValueType.HasValue:
                        //// TODO: TBD: on second thought, I think this is probably a sensible allowance.
                        //throw ThrowMaximumValueCount(Required, Optional);
                        break;
                    default:
                        if (value > 1 && !ValueType.HasValue)
                        {
                            throw ThrowMaximumValueCount();
                        }

                        break;
                }

                _maximumValueCount = value;
            }
        }

        // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
        /// <summary>
        /// <see cref="Names"/> readonly backing field.
        /// </summary>
        private readonly string[] _names;

        /// <inheritdoc />
        public IReadOnlyList<string> Names => _names;

        /// <summary>
        /// <see cref="Separators"/> readonly backing field.
        /// </summary>
        private readonly char[] _separators;

        /// <inheritdoc />
        public IReadOnlyList<char> Separators => _separators;

        /// <summary>
        /// Protected Constructor.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <inheritdoc />
        protected Option(string prototype, string description)
            : this(prototype, description, 1)
        {
        }

        /// <summary>
        /// Protected Constructor.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="maximumValueCount"></param>
        protected Option(string prototype, string description, int maximumValueCount)
        {
            Description = description;

            ParsePrototype(Prototype = prototype, out _names, out _valueType, out _separators);

            MaximumValueCount = maximumValueCount;

            if (Names.Any(x => x == AngleBrackets)
                && ((Names.Count == 1 && !ValueType.HasValue)
                    || (Names.Count > 1 && MaximumValueCount > 1)))
            {
                throw new ArgumentException(
                    $"The default option handler '{AngleBrackets}' cannot require values."
                    , nameof(prototype));
            }
        }

        // ReSharper disable InconsistentNaming
        internal static class RegexGroupNames
        {
            /// <summary>
            /// &quot;prev&quot;
            /// </summary>
            internal const string prev = nameof(prev);

            /// <summary>
            /// &quot;last&quot;
            /// </summary>
            internal const string last = nameof(last);

            /// <summary>
            /// &quot;roo&quot;
            /// </summary>
            internal const string roo = nameof(roo);

            /// <summary>
            /// &quot;sep&quot;
            /// </summary>
            internal const string sep = nameof(sep);
        }
        // ReSharper restore InconsistentNaming

        private Regex PrototypeRegex { get; } = new Regex(
            @"^(?<prev>(([A-Za-z]([\w-]|[^:=])*)[|])*)(?<last>([A-Za-z]([\w-]|[^:=])*))(?<roo>[:=])?(({(?<sep>[^\w-]+)})|(?<sep>[^\w-]+))?$"
            , Compiled);

        private void ParsePrototype(string prototype, out string[] names, out OptionValueType? parsedType, out char[] separators)
        {
            if (IsNullOrEmpty(prototype))
            {
                throw new ArgumentNullException(nameof(prototype), "Prototype must contain an option specification.");
            }

            var match = PrototypeRegex.Match(prototype);

            if (!match.Success)
            {
                throw new ArgumentException($"Invalid prototype specification: `{prototype}'.", nameof(prototype));
            }

            names = match.GetGroupValueOrDefault(prev, "")
                .Split(new[] {Pipe}, RemoveEmptyEntries)
                .ToArray();

            var lastName = match.GetGroupValue(last);

            if (lastName != null)
            {
                names = names.Concat(new[] {lastName}).ToArray();
            }

            if (!names.Any())
            {
                throw new ArgumentException("One or more argument names expected.", nameof(prototype));
            }

            parsedType = match.GetGroupValueOrDefault(roo).ToOptionValueType();

            separators = match.GetGroupValueOrDefault(sep, DefaultSeparators).Distinct().ToArray();
        }

        /// <summary>
        /// Parses the <paramref name="value"/> given the <paramref name="context"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <returns></returns>
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
                        context.Set.Localizer("Could not convert string `{0}' to type {1} for option `{2}'."),
                        value, targetType.Name, context.OptionName),
                    context.OptionName, ex);
            }

            return defaultValue;
        }

        /// <summary>
        /// Invokes the Option given the <paramref name="context"/>.
        /// </summary>
        /// <param name="context"></param>
        public void Visit(OptionContext context) => OnVisitation(context);

        /// <summary>
        /// Occurs in an Option specific manner given <paramref name="context"/>.
        /// </summary>
        /// <param name="context"></param>
        protected abstract void OnVisitation(OptionContext context);

        public override string ToString() => Prototype;
    }
}
