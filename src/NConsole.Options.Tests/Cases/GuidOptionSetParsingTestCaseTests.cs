using System;
using System.Collections.Generic;

namespace NConsole.Options.Cases
{
    using Data.Parsing.Targets;
    using Xunit.Abstractions;

    public class GuidOptionSetParsingTestCaseTests : RequiredOrOptionalOptionSetParsingTestCaseTestFixtureBase<Guid, char>
    {
        protected override IEnumerable<object[]> Cases { get; } = new GuidOptionSetParsingTestCases();

        public GuidOptionSetParsingTestCaseTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
