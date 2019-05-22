using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.KeyValue
{
    using Kingdom.Combinatorics.Combinatorials;
    using Targets;
    using static Domain;
    using static String;

    internal abstract class RequiredOrOptionalOptionSetParsingTestCasesBase<TKey, TValue>
        : RequiredOrOptionalOptionSetParsingTestCasesBase<KeyValuePair<TKey, TValue>>
    {
        protected abstract IEnumerable<TKey> NominalPairKeys { get; }

        // TODO: TBD: may refactor NominalValues to base class as well...
        protected abstract IEnumerable<TValue> NominalPairValues { get; }

        // TODO: TBD: may also refactor this to base class only, and to property instead of method...
        protected override IEnumerable<KeyValuePair<TKey, TValue>> NominalValues
        {
            get
            {
                var combinations = NominalPairKeys.Combine(NominalPairValues);

                combinations.SilentOverflow = true;

                foreach (var current in combinations)
                {
                    var key = (TKey) current[0];
                    var value = (TValue) current[1];
                    yield return KeyValuePair.Create(key, value);
                }
            }
        }

        /// <summary>
        /// Returns the <paramref name="key"/> rendered in useful <see cref="string"/> format.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected abstract string RenderKey(TKey key);

        /// <summary>
        /// Not to be confused with
        /// <see cref="RequiredOrOptionalOptionSetParsingTestCasesBase{T}.RenderValue"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract string RenderValue(TValue value);

        protected sealed override IEnumerable<string> RenderValue(KeyValuePair<TKey, TValue> pair)
        {
            var (key, value) = pair;
            yield return RenderKey(key);
            yield return RenderValue(value);
        }

        protected override IEnumerable<RenderPrototypeCasesDelegate<KeyValuePair<TKey, TValue>>> RenderCaseCallbacks
        {
            get
            {
                string RenderRequiredOrOptional(char? value) => value.HasValue ? $"{value.Value}" : "";

                string RenderParts(params string[] parts) => Join(DefaultSeparator, parts);

                // We will assume a Default Separator for purposes of unit tests.
                IEnumerable<string> RenderFullyBundledCase(string prefix, string prototypeName,
                    char? requiredOrOptional, KeyValuePair<TKey, TValue> value)
                {
                    var rendered = RenderValue(value).ToArray();
                    yield return $"{prefix}{prototypeName}{RenderRequiredOrOptional(requiredOrOptional)}{RenderParts(rendered)}";
                }

                IEnumerable<string> RenderPartiallyBundledCase(string prefix, string prototypeName,
                    char? requiredOrOptional, KeyValuePair<TKey, TValue> value)
                {
                    var rendered = RenderValue(value).ToArray();
                    yield return $"{prefix}{prototypeName}{RenderRequiredOrOptional(requiredOrOptional)}{rendered[0]}";
                    yield return rendered[1];
                }

                IEnumerable<string> RenderIndividualPartsCase(string prefix, string prototypeName,
                    char? requiredOrOptional, KeyValuePair<TKey, TValue> value)
                {
                    var rendered = RenderValue(value).ToArray();
                    yield return $"{prefix}{prototypeName}";
                    yield return rendered[0];
                    yield return rendered[1];
                }

                yield return RenderFullyBundledCase;
                yield return RenderPartiallyBundledCase;
                yield return RenderIndividualPartsCase;
            }
        }
    }
}
