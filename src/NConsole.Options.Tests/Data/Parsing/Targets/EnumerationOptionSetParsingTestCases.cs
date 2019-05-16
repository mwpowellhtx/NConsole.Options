using System;
using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.Targets
{
    /// <summary>
    /// We could use any Ordinal based Enumeration for this purpose, but we will use
    /// <see cref="StringComparison"/> for test purposes, as it is pretty much stock,
    /// and has a sufficiently nominal range of values to make testing worthwhile.
    /// </summary>
    internal class EnumerationOptionSetParsingTestCases
        : RequiredOrOptionalOptionSetParsingTestCasesBase<StringComparison>
    {
        private static string RenderValue(StringComparison value) => value.ToString();

        // ReSharper disable once IdentifierTypo
        protected override string RenderOneWordUnbunbledArgument(string prefix, string prototype
            , char requiredOrOptional, StringComparison value)
            => $"{prefix}{prototype}{requiredOrOptional}{RenderValue(value)}";

        protected override string RenderTwoWordValuePhrase(StringComparison value) => RenderValue(value);

        protected override IEnumerable<StringComparison> GetNominalValueRange()
        {
            foreach(StringComparison value in Enum.GetValues(typeof(StringComparison)))
            {
                yield return value;
            }
        }
    }
}
