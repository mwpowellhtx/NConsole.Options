using System;
using System.Collections.Generic;

namespace NConsole.Options.Cases
{
    using Data.Parsing.Targets;
    using Xunit.Abstractions;

    public class EnumerationTestCaseTests : RequiredOrOptionalTestCaseTestFixtureBase<StringComparison, char>
    {
        protected override IEnumerable<object[]> Cases { get; } = new EnumerationOptionSetParsingTestCases();

        public EnumerationTestCaseTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
