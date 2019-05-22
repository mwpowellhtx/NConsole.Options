using System;
using System.Collections.Generic;

namespace NConsole.Options.Cases
{
    using Data.Parsing.Targets;
    using Xunit.Abstractions;

    public class IntegerTestCaseTests : RequiredOrOptionalTestCaseTestFixtureBase<int, char>
    {
        protected override IEnumerable<object[]> Cases { get; } = new IntegerOptionSetParsingTestCases();

        public IntegerTestCaseTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
