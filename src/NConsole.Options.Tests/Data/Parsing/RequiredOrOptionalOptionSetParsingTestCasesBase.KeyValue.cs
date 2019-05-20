using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing
{
    internal abstract class RequiredOrOptionalOptionSetParsingTestCasesBase<TKey, TValue>
        : RequiredOrOptionalOptionSetParsingTestCasesBase<KeyValuePair<TKey, TValue>>
    {
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
    }
}
