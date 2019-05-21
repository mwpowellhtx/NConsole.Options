using System.Collections.Generic;

namespace NConsole.Options.Cases
{
    using Data.Parsing.Targets;
    using Xunit.Abstractions;

    public class BooleanTestCaseTests : RequiredOrOptionalTestCaseTestFixtureBase<bool, char>
    {
        protected override IEnumerable<object[]> Cases { get; } = new BooleanOptionSetParsingTestCases();

        public BooleanTestCaseTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
