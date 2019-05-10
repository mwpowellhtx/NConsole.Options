using System;

namespace NConsole.Options
{
    using Xunit.Abstractions;

    public class LongGuidKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<long, Guid>
    {
        public LongGuidKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
