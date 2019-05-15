using System;

namespace NConsole.Options.Registration.KeyValue
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
