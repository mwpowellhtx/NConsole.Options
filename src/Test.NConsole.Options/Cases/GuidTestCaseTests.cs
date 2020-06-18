using System;
using System.Collections.Generic;

namespace NConsole.Options.Cases
{
    using Data.Parsing.Targets;
    using Xunit.Abstractions;

    public class GuidTestCaseTests : RequiredOrOptionalTestCaseTestFixtureBase<Guid, char>
    {
        protected override IEnumerable<object[]> Cases { get; } = new GuidOptionSetParsingTestCases();

        public GuidTestCaseTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
