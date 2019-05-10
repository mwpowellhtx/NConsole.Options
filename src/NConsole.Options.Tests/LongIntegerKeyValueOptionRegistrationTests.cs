namespace NConsole.Options
{
    using Xunit.Abstractions;

    public class LongIntegerKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<long, int>
    {
        public LongIntegerKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
