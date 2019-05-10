using System;

namespace NConsole.Options
{
    using Xunit.Abstractions;

    public class StringStringComparisonKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<string, StringComparison>
    {
        public StringStringComparisonKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
