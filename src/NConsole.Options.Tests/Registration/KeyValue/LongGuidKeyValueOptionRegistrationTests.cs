using System;

namespace NConsole.Options.Registration.KeyValue
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
