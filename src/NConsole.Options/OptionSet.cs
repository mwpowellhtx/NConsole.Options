using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NConsole.Options
{
    using static Char;
    using static Characters;
    using static Domain;
    using static Math;
    using static OptionSet.RegularExpressionNames;
    using static String;
    using static OptionValueType;
    using static StringComparison;
    using static RegexOptions;
    using LocalizationCallback = Converter<string, string>;

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
        public OptionSet(LocalizationCallback localizer)
        {
            Localizer = localizer;
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Gets the MessageLocalizer.
        /// </summary>
        public LocalizationCallback Localizer { get; }
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

        private OptionSet Add(Delegate callback, Func<Option> optionFactory)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return Add(optionFactory.Invoke());
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
            => Add(callback
                // ReSharper disable once ConvertClosureToMethodGroup
                , () => new SimpleActionOption(prototype, description, () => callback.Invoke())
            );

        /// <summary>
        /// Adds the <see cref="ActionOption{TTarget}"/> corresponding with the arguments to the
        /// end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, OptionCallback<string> callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="ActionOption{TTarget}"/> corresponding with the arguments to the
        /// end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, string description, OptionCallback<string> callback)
            => Add<string>(prototype, description, callback);

        /// <summary>
        /// Adds the <see cref="ActionOption{TTarget}"/> corresponding with the arguments to the
        /// end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add<T>(string prototype, OptionCallback<T> callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="ActionOption{TTarget}"/> corresponding with the arguments to the
        /// end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add<T>(string prototype, string description, OptionCallback<T> callback)
            => Add(callback
                // ReSharper disable once ConvertClosureToMethodGroup
                , () => new ActionOption<T>(prototype, description, v => callback.Invoke(v))
            );

        /// <summary>
        /// Adds the <see cref="KeyValueActionOption{TKey,TValue}"/> corresponding with the
        /// arguments to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, OptionCallback<string, string> callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="KeyValueActionOption{TKey,TValue}"/> corresponding with the
        /// arguments to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add(string prototype, string description, OptionCallback<string, string> callback)
            => Add<string, string>(prototype, description, callback);

        /// <summary>
        /// Adds the <see cref="KeyValueActionOption{TKey,TValue}"/> corresponding with the
        /// arguments to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add<TKey, TValue>(string prototype, OptionCallback<TKey, TValue> callback)
            => Add(prototype, null, callback);

        /// <summary>
        /// Adds the <see cref="KeyValueActionOption{TKey,TValue}"/> corresponding with the
        /// arguments to the end of the Collection.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public OptionSet Add<TKey, TValue>(string prototype, string description, OptionCallback<TKey, TValue> callback)
            => Add(callback
                // ReSharper disable once ConvertClosureToMethodGroup
                , () => new KeyValueActionOption<TKey, TValue>(prototype, description, (k, v) => callback.Invoke(k, v))
            );

        protected virtual OptionContext CreateOptionContext() => new OptionContext(this);

        // TODO: TBD: the execution on this (baseline?) implementation is REALLY confused, I think.
        // TODO: TBD: I think it needs to be simplified: 1) stage the argument parts for the desired option(s)
        // TODO: TBD: 2) deliver said parts to the matching option(s) "on visitation" ...
        // TODO: TBD: 3) capture any parts that fail to match anything

        private Option DefaultOption => Contains(AngleBrackets) ? this[AngleBrackets] : null;

        /// <summary>
        /// Parses the <paramref name="args"/> and returns any that were Not Dispatched
        /// during the operation. Calling this method is the nerve center of starting the
        /// <see cref="Option"/> collection being parsed, doing something with them in the
        /// scope of your application.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<string> Parse(params string[] args)
        {
            var argsNotDispatched = new List<string>();

            var context = CreateOptionContext();

            for (var count = 0; args.Any(); args = args.Skip(count + 1).ToArray())
            {
                var arg = args[0];

                // As informed by the Default value and by subsequent Dispatch.
                switch (count)
                {
                    case 0:
                        ++context.OptionIndex;
                        break;

                    default:
                        // TODO: TBD: check this, this is probably correct... or is that ... += (count - 1) ?
                        context.OptionIndex += count + 1;
                        break;
                }

                // TODO: TBD: should a similar thing happen for Slash? Dash?
                // Ignoring non-arguments.
                if (arg == DoubleDash)
                {
                    argsNotDispatched.Add(arg);
                    count = 0;
                    continue;
                }

                // Evaluate the Bundle first since its pattern is a Subset of the Singular Argument.
                if (!(TryEvaluateBundle(arg, out var parts) || TryEvaluateArgument(arg, out parts)))
                {
                    // If the pattern is Neither of these, then Report as such and Continue.
                    argsNotDispatched.Add(arg);
                    continue;
                }

                // TODO: TBD: I do not thing this necessarily needs to be a Try...
                if (!TryDispatchOptionVisit(context, ref parts, args, out count))
                {
                    args.Take(count + 1).ToList().ForEach(argsNotDispatched.Add);
                }
            }

            return argsNotDispatched;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="parts"></param>
        /// <param name="args"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool TryDispatchOptionVisit(OptionContext context, ref ArgumentEvaluationResult parts
            , IReadOnlyList<string> args, out int count)
        {
            var dispatched = 0;

            count = 0;

            string RenderEnableBoolean(bool enable) => $"{enable}".ToLower();

            bool IsNeitherBundleNorArgument(string arg) => !(TryEvaluateBundle(arg, out _) || TryEvaluateArgument(arg, out _));

            bool AreNeitherBundlesNorArguments(params string[] items) => items.All(IsNeitherBundleNorArgument);

            // Key is more of a Key at this point than a Name, used to lookup the Option.
            foreach (var (key, option) in parts.Options)
            {
                // Accounting for Parts.Value, Boolean Flags, is transparent with this approach.
                var currentCount = 0;

                /* Having closed the loop with this bookkeeping, then we can focus our undivided
                 * attention on Dispatching the correct Option Parameters to the next Option. */

                context.Option = option;
                context.OptionName = $"{parts.Flag}{key}";
                context.OptionValues.Clear();

                /* TODO: TBD: the dispatch heuristics are a more relax here, just simply because each
                 * individual Option may require a different set of parameters, but we still want to
                 * signal which actual arguments were consumed by either the dispatched bundled value,
                 * or the next value(s) in sequence, etc. */

                // Determine which other Parameters we should be able to furnish the next Dispatch.
                switch (context.Option)
                {
                    // No-op in this case, we have what we came here for.
                    case ISimpleActionOption _:
                        break;

                    case IActionOption _ when parts.HasValue && !parts.EnableBoolean.HasValue:
                        context.OptionValues.Add(parts.Value);
                        break;

                    case IActionOption _ when parts.EnableBoolean.HasValue && !parts.HasValue:
                        context.OptionValues.Add(RenderEnableBoolean(parts.EnableBoolean.Value));
                        break;

                    case IActionOption _ when !(parts.HasValue || parts.EnableBoolean.HasValue)
                                              && args.Count > 1
                                              && IsNeitherBundleNorArgument(args[1]):

                        context.OptionValues.Add(args[++currentCount]);
                        break;

                    // TODO: TBD: we will worry about additional 'separator' based key/value cases in a later iteration.
                    case IKeyValueActionOption _ when parts.HasValue && !parts.EnableBoolean.HasValue
                                                      && args.Count > 1
                                                      && IsNeitherBundleNorArgument(args[1]):

                        context.OptionValues.Add(parts.Value);
                        context.OptionValues.Add(args[++currentCount]);
                        break;

                    case IKeyValueActionOption _ when parts.EnableBoolean.HasValue && !parts.HasValue
                                                      && args.Count > 1
                                                      && IsNeitherBundleNorArgument(args[1]):

                        context.OptionValues.Add(RenderEnableBoolean(parts.EnableBoolean.Value));
                        context.OptionValues.Add(args[++currentCount]);
                        break;

                    case IKeyValueActionOption _ when !(parts.HasValue || parts.EnableBoolean.HasValue)
                                                      && args.Count > 2
                                                      && AreNeitherBundlesNorArguments(args[1], args[2]):

                        context.OptionValues.Add(args[++currentCount]);
                        context.OptionValues.Add(args[++currentCount]);
                        break;

                    default:

                        //throw new OptionException(
                        //    Format(Localizer("Invalid `{0}' parameter specification discovered.")
                        //        , context.OptionName)
                        //    , context.OptionName
                        //);

                        // TODO: TBD: throw? or simply continue?
                        // There is nothing to process, so Continue, bypass the Visitation.
                        continue;
                }

                // Which culminates in an Option Visit.
                context.Visit();

                // Pull the Maximum Count forward to be reconciled by the Caller.
                count = Max(count, currentCount);

                ++dispatched;
            }

            return dispatched > 0;
        }

        private struct ArgumentEvaluationResult
        {
            internal string Flag { get; }

            /// <summary>
            /// Gets the Name. &quot;Name&quot; represents either the Name, or Bundled
            /// <see cref="Option.Names"/> plus potentially a value. Additionally, Name
            /// ignores any <see cref="BooleanFlags"/> concerns appended to itself.
            /// </summary>
            /// <see cref="EnableBoolean"/>
            internal string Name { get; }

            /// <summary>
            /// Gets the Options involved Keyed based on the Name used to identify the Value.
            /// The String of which is more of a Key at this point than a Name, which was used
            /// to lookup the actual <see cref="Option"/> instance.
            /// </summary>
            internal ICollection<Tuple<string, Option>> Options { get; }

            // TODO: TBD: what to do about Separator ...
            internal string Separator { get; }

            /// <summary>
            /// Gets or Sets the Value.
            /// </summary>
            internal string Value { get; set; }

            /// <summary>
            /// Feeds the <see cref="EnableBoolean"/> indicator.
            /// </summary>
            /// <see cref="EnableBoolean"/>
            private Lazy<bool?> LazyEnableBoolean { get; }

            /// <summary>
            /// Gets the <see cref="Lazy{T}"/> initialized Boolean whether to Enable the Option.
            /// Optionally indicates whether to Enable or Disable the Option accordingly.
            /// </summary>
            /// <see cref="BooleanFlags"/>
            /// <see cref="LazyEnableBoolean"/>
            internal bool? EnableBoolean => LazyEnableBoolean.Value;

            // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
            internal static readonly ArgumentEvaluationResult Failed = new ArgumentEvaluationResult { };

            // ReSharper disable once IdentifierTypo
            /// <summary>
            /// Internal Constructor.
            /// </summary>
            /// <param name="flag"></param>
            /// <param name="name"></param>
            /// <param name="separator"></param>
            /// <param name="value"></param>
            internal ArgumentEvaluationResult(string flag = null, string name = null, string separator = null, string value = null)
            {
                // Defer evaluation of the Last character in the Name enumerated value.
                char NameLast() => name.Last();

                Flag = flag;

                // Set the Name without any appended Boolean Flags.
                Name = IsNullOrEmpty(name)
                    ? null
                    : BooleanFlags.Contains(NameLast())
                        ? name.Substring(0, name.Length - 1)
                        : name;

                // TODO: TBD: what to do about 'Separators'
                Separator = separator;

                // TODO: TBD: which Value may be the a special case of Enable Boolean.
                Value = value;

                LazyEnableBoolean = new Lazy<bool?>(() =>
                {
                    //                                                 +    (true),  -    (false)
                    var booleanFlagsMap = new Dictionary<char, bool> {{Plus, true}, {Dash, false}};
                    return !IsNullOrEmpty(name)
                           && booleanFlagsMap.TryGetValue(NameLast(), out var x)
                        ? x
                        : (bool?) null;
                });

                // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
                Options = new List<Tuple<string, Option>> { };
            }

            /// <summary>
            /// Gets whether the Evaluation IsValid. Valid when we Have a Flag and a Name.
            /// Additionally, we do not expect BOTH <see cref="Value"/> and
            /// <see cref="EnableBoolean"/> to have occurred.
            /// </summary>
            internal bool IsValid => !(IsNullOrEmpty(Flag) && IsNullOrEmpty(Name))
                                     && !(!IsNullOrEmpty(Value) && EnableBoolean.HasValue);

            internal bool IsBundled => Flag == $"{Dash}" && !IsNullOrEmpty(Name);

            /// <summary>
            /// Gets whether HasValue. True when <see cref="Value"/> actually Has a Value,
            /// or when <see cref="EnableBoolean"/> Has a Value.
            /// </summary>
            /// <see cref="Value"/>
            /// <see cref="EnableBoolean"/>
            internal bool HasValue => !IsNullOrEmpty(Value) || EnableBoolean.HasValue;
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
            /// &quot;bundle&quot;
            /// </summary>
            public const string bundle = nameof(bundle);

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
        // TODO: TBD: handling "bundled" as a separate case...
        private Regex BundledOptionRegex { get; } = new Regex(@"^((?!--|/)(?<flag>-))(?<bundle>[^:=]+)?$", Compiled);

        private bool TryEvaluateBundle(string arg, out ArgumentEvaluationResult result)
        {
            // TODO: TBD: we might even "evaluate" the Option (or Options) depending on the evaluated result.
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg));
            }

            var match = BundledOptionRegex.Match(arg);

            result = match.Success && match.AreGroupsSuccessful(flag, bundle)
                    ? new ArgumentEvaluationResult(match.GetGroupValue(flag), match.GetGroupValue(bundle))
                    : ArgumentEvaluationResult.Failed;

            if (!(result.IsValid || result.IsBundled))
            {
                return false;
            }

            /* We could interpolate each of the characters then evaluate Contains and Select,
             * but this works as well, and only Takes what we want. */
            foreach (var y in result.Name.TakeWhile(x => Contains($"{x}")).Select(x => $"{x}"))
            {
                result.Options.Add(Tuple.Create(y, this[y]));
            }

            result.Value = result.Name.Length == result.Options.Count
                ? null
                : result.Name.Substring(result.Options.Count);

            return true;
        }

        private Regex ValueOptionRegex { get; } = new Regex(@"^(?<flag>--|-|/)(?<name>[^:=]+)((?<sep>[:=])(?<val>.*))?$", Compiled);

        private bool TryEvaluateArgument(string arg, out ArgumentEvaluationResult result)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg));
            }

            var match = ValueOptionRegex.Match(arg);

            result = match.Success && match.AreGroupsSuccessful(flag, name)
                ? new ArgumentEvaluationResult(match.GetGroupValue(flag), match.GetGroupValue(name)
                    , match.GetGroupValueOrDefault(sep), match.GetGroupValueOrDefault(val))
                : ArgumentEvaluationResult.Failed;

            if (!result.IsValid)
            {
                return false;
            }

            result.Options.Add(Tuple.Create(
                result.Name
                , Contains(result.Name) ? this[result.Name] : DefaultOption
            ));

            return true;
        }

        // TODO: TBD: next steps might include evaluating whether description formatting ought to be any different...
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

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (option.ValueType)
            {
                case Optional:
                case Required:
                {
                    if (option.ValueType == Optional)
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

                    if (option.ValueType == Optional)
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
                    start = description.IndexOf(name, j, InvariantCulture);
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
