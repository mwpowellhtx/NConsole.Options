using System;
using System.Collections.Generic;

namespace NConsole.Options.Cases
{
    using Data.Parsing.Targets;
    using Xunit.Abstractions;

    public class EnumerationOptionSetParsingTestCaseTests : RequiredOrOptionalOptionSetParsingTestCaseTestFixtureBase<StringComparison, char>
    {
        protected override IEnumerable<object[]> Cases { get; } = new EnumerationOptionSetParsingTestCases();

        public EnumerationOptionSetParsingTestCaseTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
