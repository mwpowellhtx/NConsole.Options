using System;
using System.Collections.Generic;

namespace NConsole.Options.Cases
{
    using Data.Parsing.Targets;
    using Xunit.Abstractions;

    public class IntegerOptionSetParsingTestCaseTests : RequiredOrOptionalOptionSetParsingTestCaseTestFixtureBase<int, char>
    {
        protected override IEnumerable<object[]> Cases { get; } = new IntegerOptionSetParsingTestCases();

        public IntegerOptionSetParsingTestCaseTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
