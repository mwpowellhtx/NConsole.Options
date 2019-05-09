using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NConsole.Options
{
    using static Char;
    using static Characters;
    using static Domain;
    using static OptionSet.RegularExpressionNames;
    using static String;

    /// <inheritdoc />
    public class OptionSet : KeyedCollection<string, Option>
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <inheritdoc />
        public OptionSet() : this(s => s)
        {
        }

        // ReSharper disable IdentifierTypo
        /// <summary>
        /// <see cref="Converter{TInput,TOutput}"/> Constructor.
        /// </summary>
        /// <param name="localizer"></param>
        /// <inheritdoc />
        public OptionSet(Converter<string, string> localizer)
        {
            Localizer = localizer;
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Gets the MessageLocalizer.
        /// </summary>
        public Converter<string, string> Localizer { get; }
        // ReSharper restore IdentifierTypo

        /// <inheritdoc />
        protected override string GetKeyForItem(Option item)
        {
            var option = item;

            if (option == null)
            {
                throw new ArgumentNullException(nameof(option));
            }

            if (option.Names != null && option.Names.Length > 0)
            {
                return option.Names[0];
            }

            // This should never happen since it is invalid for Option to be without any names.
            throw new InvalidOperationException($"`{nameof(option)}' has no names!");
        }

        //[Obsolete("Use KeyedCollection.this[string]")]
        //protected Option GetOptionForName(string option)
        //{
        //    if (option == null)
        //    {
        //        throw new ArgumentNullException(nameof(option));
        //    }
        //    try
        //    {
        //        return base[option];
        //    }
        //    // ReSharper disable once IdentifierTypo
        //    catch (KeyNotFoundException knfex)
        //    {
        //        return null;
        //    }
        //}

        /// <inheritdoc />
        protected override void InsertItem(int index, Option item)
        {
            base.InsertItem(index, item);
            AddOption(item);
        }

        /// <inheritdoc />
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            var option = Items[index];
            // KeyedCollection.RemoveItem() handles the 0th item
            for (var i = 1; i < option.Names.Length; ++i)
            {
                Dictionary.Remove(option.Names[i]);
            }
        }

        /// <inheritdoc />
        protected override void SetItem(int index, Option item)
        {
            base.SetItem(index, item);
            RemoveItem(index);
            AddOption(item);
        }

        /// <summary>
        /// Adds the <paramref name="option"/> to the end of the Collection.
        /// </summary>
        /// <param name="option"></param>
        private void AddOption(Option option)
        {
            if (option == null)
            {
                throw new ArgumentNullException(nameof(option));
            }

            var added = new List<string>(option.Names.Length);
            try
            {
                // KeyedCollection.InsertItem/SetItem handle the 0th name.
                for (var i = 1; i < option.Names.Length; ++i)
                {
                    Dictionary.Add(option.Names[i], option);
                    added.Add(option.Names[i]);
                }
            }
            catch (Exception ex)
            {
                foreach (var name in added)
                {
                    Dictionary.Remove(name);
                }

                throw;
            }
        }

        /// <summary>
        /// Adds an <paramref name="option"/> to the end of the Collection. Note that mask
        /// the Add API here because we do not want the base class handling any arbitrary
        /// <see cref="Option"/> requests, even though the constructors are internal. Also,
        /// because we want the API to return the <see cref="OptionSet"/> instance.
        /// </summary>
        /// <param name="option">The <see cref="Option"/> to be added to the end
        /// of the Collection.</param>
        /// <returns></returns>
        /// <see cref="Collection{T}.Add"/>
        public new OptionSet Add(Option option)
        {
            base.Add(option);
            return this;
        }

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, OptionCallback callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, string description, OptionCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            // ReSharper disable once ConvertClosureToMethodGroup
            return Add(new SimpleActionOption(prototype, description, () => callback.Invoke()));
        }

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, OptionCallback<string> callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, string description, OptionCallback<string> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return Add(new ValueCollectionActionOption(prototype, description, 1, val => callback.Invoke(val[0])));
        }

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, OptionCallback<string, string> callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, string description, OptionCallback<string, string> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return Add(new KeyValueActionOption<string, string>(prototype, description, callback));
        }

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add<T>(string prototype, OptionCallback<T> callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add<T>(string prototype, string description, OptionCallback<T> callback)
            => Add(new ActionOption<T>(prototype, description, callback));

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add<TKey, TValue>(string prototype, OptionCallback<TKey, TValue> callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="Option"/> corresponding with the arguments
        /// to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add<TKey, TValue>(string prototype, string description, OptionCallback<TKey, TValue> callback)
            => Add(new KeyValueActionOption<TKey, TValue>(prototype, description, callback));

        protected virtual OptionContext CreateOptionContext() => new OptionContext(this);

        /// <summary>
        /// Parses the <paramref name="args"/> and returns any Unprocessed Arguments encountered
        /// during the operation. Calling this method is the nerve center of starting the
        /// <see cref="Option"/> collection being parsed, doing something with them in the
        /// scope of your application.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<string> Parse(params string[] args)
        {
            // TODO: TBD: this is when we use context? any other time? should it be disposable in any way?
            // TODO: TBD: I am thinking that "context" should potentially be more broadly cross-cutting than that...
            // TODO: TBD: the reason being is that there are moment in the Option, callbacks, etc, where there are strings which could benefit from localization...
            var context = CreateOptionContext();
            // TODO: TBD: consider how Context itself should be responsible for processing across the range of option set... ?
            // TODO: TBD: would be a subtle, but potentially worthwhile, refactoring, that inverts the control, provides opportunities for functional injection, etc
            context.OptionIndex = -1;
            var process = true;
            var defaultOption = Contains(AngleBrackets) ? this[AngleBrackets] : null;
            var unprocessed = new List<string>();
            foreach (var arg in args)
            {
                ++context.OptionIndex;
                if (arg == DoubleDash)
                {
                    process = false;
                    continue;
                }

                // TODO: TBD: ism's like, !process, !parse, falling through to `Unprocessed', just refactor that to a more natural course of evaluation, sequence of TryProcess...
                if (!process)
                {
                    TryProcessArgument(arg, defaultOption, context, unprocessed);
                    continue;
                }

                if (!TryParse(arg, context))
                {
                    TryProcessArgument(arg, defaultOption, context, unprocessed);
                }
            }

            context.Option?.Invoke(context);

            return unprocessed;
        }

        //public List<string> Parse2(IEnumerable<string> args)
        //{
        //    var process = true;
        //    var context = CreateOptionContext();
        //    context.OptionIndex = -1;
        //    var def = GetOptionForName(AngleBrackets);
        //    var unprocessed = args.Where(x =>
        //        ++context.OptionIndex < 0
        //        || !process && def == null
        //        || (process
        //            ? x == $"{Dash}{Dash}"
        //                ? process = false
        //                : !Parse(x, context) && (def == null || Unprocessed(null, def, context, x))
        //            : def == null || Unprocessed(null, def, context, x)));
        //    var r = unprocessed.ToList();
        //    context.Option?.Invoke(context);
        //    return r;
        //}

        /// <summary>
        /// Tries to Process the <paramref name="arg"/> given the parameters.
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="candidateOption"></param>
        /// <param name="context"></param>
        /// <param name="remaining"></param>
        /// <returns></returns>
        private static bool TryProcessArgument(string arg, Option candidateOption, OptionContext context, ICollection<string> remaining)
        {
            if (candidateOption == null)
            {
                remaining.Add(arg);
                return false;
            }

            context.OptionValues.Add(arg);
            context.Option = candidateOption;
            context.Option.Invoke(context);
            // TODO: TBD: why are we returning False here as well?
            return false;
        }

        // ReSharper disable InconsistentNaming
        internal static class RegularExpressionNames
        {
            /// <summary>
            /// &quot;flag&quot;
            /// </summary>
            public const string flag = nameof(flag);

            /// <summary>
            /// &quot;name&quot;
            /// </summary>
            public const string name = nameof(name);

            /// <summary>
            /// &quot;sep&quot;
            /// </summary>
            public const string sep = nameof(sep);

            /// <summary>
            /// &quot;val&quot;
            /// </summary>
            public const string val = nameof(val);
        }
        // ReSharper restore InconsistentNaming

        private Regex ValueOptionRegex { get; } = new Regex(
            @"^(?<flag>--|-|/)(?<name>[^:=]+)((?<sep>[:=])(?<val>.*))?$"
            , RegexOptions.Compiled
        );

        protected bool TryGetOptionParts(string arg, out string flagResult
            , out string nameResult, out string sepResult, out string valResult)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg));
            }

            flagResult = nameResult = sepResult = valResult = null;
            var m = ValueOptionRegex.Match(arg);
            if (!m.Success)
            {
                return false;
            }

            flagResult = m.Groups[flag].Value;
            nameResult = m.Groups[name].Value;

            // ReSharper disable once InvertIf
            if (m.Groups[sep].Success && m.Groups[val].Success)
            {
                sepResult = m.Groups[sep].Value;
                valResult = m.Groups[val].Value;
            }

            return true;
        }

        protected virtual bool TryParse(string arg, OptionContext context)
        {
            if (context.Option != null)
            {
                ParseValue(arg, context);
                return true;
            }

            if (!TryGetOptionParts(arg, out var flag, out var name, out var sep, out var val))
            {
                return false;
            }

            // ReSharper disable once InvertIf
            if (Contains(name))
            {
                var option = this[name];

                context.OptionName = $"{flag}{name}";
                context.Option = option;

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (option.ValueType)
                {
                    case OptionValueType.None:
                        context.OptionValues.Add(name);
                        context.Option.Invoke(context);
                        break;

                    case OptionValueType.Optional:
                    case OptionValueType.Required:
                        ParseValue(val, context);
                        break;
                }

                return true;
            }

            // TODO: TBD: No match; is it a bool option?
            // TODO: TBD: Is it a bundled option?
            return ParseBool(arg, name, context)
                   || ParseBundledValue(flag, $"{name}{sep}{val}", context)
                ;
        }

        private void ParseValue(string option, OptionContext context)
        {
            if (option != null)
            {
                foreach (var optionText in context.Option.ValueSeparators != null
                    ? option.Split(context.Option.ValueSeparators, StringSplitOptions.None)
                    : new[] {option})
                {
                    context.OptionValues.Add(optionText);
                }
            }

            if (context.OptionValues.Count == context.Option.MaximumValueCount
                || context.Option.ValueType == OptionValueType.Optional)
            {
                context.Option.Invoke(context);
            }
            else if (context.OptionValues.Count > context.Option.MaximumValueCount)
            {
                throw new OptionException(Format(
                        Localizer("Error: Found `{0}' option values when expecting `{1}'.")
                        , context.OptionValues.Count, context.Option.MaximumValueCount),
                    context.OptionName);
            }
        }

        private bool ParseBool(string optionText, string name, OptionContext context)
        {
            string requiredName;

            if (name.Length < 1
                || !(name[name.Length - 1] == Plus || name[name.Length - 1] == Dash)
                || !Contains(requiredName = name.Substring(0, name.Length - 1)))
            {
                return false;
            }

            var option = this[requiredName];
            var v = name[name.Length - 1] == Plus ? optionText : null;

            context.OptionName = optionText;
            context.Option = option;
            context.OptionValues.Add(v);

            option.Invoke(context);

            return true;
        }

        private bool ParseBundledValue(string flag, string name, OptionContext context)
        {
            if (flag != $"{Dash}")
            {
                return false;
            }

            for (var i = 0; i < name.Length; ++i)
            {
                var optionText = $"{flag}{name[i]}";
                var rn = name[i].ToString();
                if (!Contains(rn))
                {
                    if (i == 0)
                    {
                        return false;
                    }

                    throw new OptionException(Format(
                            Localizer("Cannot bundle unregistered option `{0}'.")
                            , optionText)
                        , optionText);
                }

                var option = this[rn];

                switch (option.ValueType)
                {
                    case OptionValueType.None:
                        Invoke(context, optionText, name, option);
                        break;

                    case OptionValueType.Optional:
                    case OptionValueType.Required:
                    {
                        var v = name.Substring(i + 1);
                        context.Option = option;
                        context.OptionName = optionText;
                        ParseValue(v.Length != 0 ? v : null, context);
                        return true;
                    }

                    default:
                        throw new InvalidOperationException("Unknown OptionValueType: " + option.ValueType);
                }
            }

            return true;
        }

        private static void Invoke(OptionContext context, string name, string value, Option option)
        {
            context.OptionName = name;
            context.Option = option;
            context.OptionValues.Add(value);
            option.Invoke(context);
        }

        private const int OptionWidth = 29;

        public void WriteOptionDescriptions(TextWriter writer)
        {
            foreach (var option in this)
            {
                var written = 0;
                if (!WriteOptionPrototype(writer, option, ref written))
                {
                    continue;
                }

                if (written < OptionWidth)
                {
                    writer.Write(new string(Space, OptionWidth - written));
                }
                else
                {
                    writer.WriteLine();
                    writer.Write(new string(Space, OptionWidth));
                }

                var indent = false;
                var prefix = new string(Space, OptionWidth + 2);
                foreach (var line in GetLines(
                    Localizer(GetDescription(option.Description))
                ))
                {
                    if (indent)
                    {
                        writer.Write(prefix);
                    }

                    writer.WriteLine(line);
                    indent = true;
                }
            }
        }

        private bool WriteOptionPrototype(TextWriter writer, Option option, ref int written)
        {
            var names = option.Names;

            var i = GetNextOptionIndex(names, 0);
            if (i == names.Length)
                return false;

            if (names[i].Length == 1)
            {
                Write(writer, ref written, $"  {Dash}");
                Write(writer, ref written, names[0]);
            }
            else
            {
                Write(writer, ref written, $"      {Dash}{Dash}");
                Write(writer, ref written, names[0]);
            }

            for (i = GetNextOptionIndex(names, i + 1); i < names.Length; i = GetNextOptionIndex(names, i + 1))
            {
                Write(writer, ref written, $"{Comma} ");
                Write(writer, ref written, names[i].Length == 1 ? $"{Dash}" : $"{Dash}{Dash}");
                Write(writer, ref written, names[i]);
            }

            switch (option.ValueType)
            {
                case OptionValueType.Optional:
                case OptionValueType.Required:
                {
                    if (option.ValueType == OptionValueType.Optional)
                    {
                        Write(writer, ref written, Localizer($"{SquareBracketOpen}"));
                    }

                    Write(writer, ref written
                        , Localizer($"{Equal}{GetArgumentName(0, option.MaximumValueCount, option.Description)}"));

                    var sep = option.ValueSeparators != null && option.ValueSeparators.Length > 0
                        ? option.ValueSeparators[0]
                        : " ";

                    for (var c = 1; c < option.MaximumValueCount; ++c)
                    {
                        Write(writer, ref written,
                            Localizer($"{sep}{GetArgumentName(c, option.MaximumValueCount, option.Description)}")
                        );
                    }

                    if (option.ValueType == OptionValueType.Optional)
                    {
                        Write(writer, ref written, Localizer($"{SquareBracketClose}"));
                    }

                    break;
                }
            }

            return true;
        }

        private static int GetNextOptionIndex(IReadOnlyList<string> names, int i)
        {
            while (i < names.Count && names[i] == AngleBrackets)
            {
                ++i;
            }

            return i;
        }

        private static void Write(TextWriter writer, ref int n, string s)
        {
            n += s.Length;
            writer.Write(s);
        }

        private static string GetArgumentName(int index, int maxIndex, string description)
        {
            var hasOneArgument = maxIndex == 1;

            if (description == null)
            {
                return hasOneArgument ? VALUE : $"{VALUE}{index + 1}";
            }

            var nameStart = hasOneArgument
                ? new[] {$"{CurlyBracesOpen}{Zed}{Colon}", $"{CurlyBracesOpen}"}
                : new[] {$"{CurlyBracesOpen}{index}{Colon}"};

            foreach (var name in nameStart)
            {
                int start, j = 0;
                do
                {
                    start = description.IndexOf(name, j);
                } while (start >= 0 && j != 0 && description[j++ - 1] == CurlyBracesOpen);

                if (start == -1)
                {
                    continue;
                }

                var end = description.IndexOf(CurlyBracesClose, start);
                if (end == -1)
                {
                    continue;
                }

                return description.Substring(start + name.Length, end - start - name.Length);
            }

            return hasOneArgument ? VALUE : $"{VALUE}{index + 1}";
        }

        private static string GetDescription(string description)
        {
            if (description == null)
            {
                return Empty;
            }

            var sb = new StringBuilder(description.Length);
            var start = -1;

            for (var i = 0; i < description.Length; ++i)
            {
                switch (description[i])
                {
                    case CurlyBracesOpen:

                        if (i == start)
                        {
                            sb.Append(CurlyBracesOpen);
                            start = -1;
                        }
                        else if (start < 0)
                        {
                            start = i + 1;
                        }

                        break;

                    case CurlyBracesClose:

                        if (start < 0)
                        {
                            if (i + 1 == description.Length || description[i + 1] != CurlyBracesClose)
                            {
                                throw new InvalidOperationException($"Invalid option description: {description}");
                            }

                            ++i;
                            sb.Append(CurlyBracesClose);
                        }
                        else
                        {
                            sb.Append(description.Substring(start, i - start));
                            start = -1;
                        }

                        break;

                    case Colon:

                        if (start < 0)
                        {
                            // TODO: TBD: huh? a goto? wow...
                            goto default;
                        }

                        start = i + 1;
                        break;

                    default:

                        if (start < 0)
                        {
                            sb.Append(description[i]);
                        }

                        break;
                }
            }

            return sb.ToString();
        }

        private static IEnumerable<string> GetLines(string description)
        {
            if (IsNullOrEmpty(description))
            {
                yield return Empty;
                yield break;
            }

            var length = 80 - OptionWidth - 1;
            int start = 0, end;
            do
            {
                end = GetLineEnd(start, length, description);
                var c = description[end - 1];
                if (IsWhiteSpace(c))
                {
                    --end;
                }

                var writeContinuation = end != description.Length && !IsEolChar(c);
                var line = description.Substring(start, end - start) + (writeContinuation ? $"{Dash}" : "");
                yield return line;
                start = end;
                if (IsWhiteSpace(c))
                {
                    ++start;
                }

                length = 80 - OptionWidth - 2 - 1;
            } while (end < description.Length);
        }

        private static bool IsEolChar(char c) => !IsLetterOrDigit(c);

        private static int GetLineEnd(int start, int length, string description)
        {
            var end = Math.Min(start + length, description.Length);
            var sep = -1;
            for (var i = start + 1; i < end; ++i)
            {
                if (description[i] == NewLine)
                {
                    return i + 1;
                }

                if (IsEolChar(description[i]))
                {
                    sep = i + 1;
                }
            }

            if (sep == -1 || end == description.Length)
            {
                return end;
            }

            return sep;
        }
    }
}
