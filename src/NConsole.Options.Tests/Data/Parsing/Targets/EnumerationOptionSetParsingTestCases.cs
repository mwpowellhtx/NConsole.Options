using System;
using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    /// <summary>
    /// We could use any Ordinal based Enumeration for this purpose, but we will use
    /// <see cref="StringComparison"/> for test purposes, as it is pretty much stock,
    /// and has a sufficiently nominal range of values to make testing worthwhile.
    /// </summary>
    internal class EnumerationOptionSetParsingTestCases
        : RequiredOrOptionalOptionSetParsingTestCasesBase<StringComparison>
    {
        protected override IEnumerable<string> RenderValue(StringComparison value) => GetRange(value.ToString());

        protected override IEnumerable<StringComparison> NominalValues
        {
            get
            {
                foreach (StringComparison value in Enum.GetValues(typeof(StringComparison)))
                {
                    yield return value;
                }
            }
        }
    }
}
