using System.Collections.Generic;

namespace NConsole.Options.Cases
{
    using Data.Parsing.Targets;
    using Xunit.Abstractions;

    public class BooleanOptionSetParsingTestCaseTests : RequiredOrOptionalOptionSetParsingTestCaseTestFixtureBase<bool, char>
    {
        protected override IEnumerable<object[]> Cases { get; } = new BooleanOptionSetParsingTestCases();

        public BooleanOptionSetParsingTestCaseTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
