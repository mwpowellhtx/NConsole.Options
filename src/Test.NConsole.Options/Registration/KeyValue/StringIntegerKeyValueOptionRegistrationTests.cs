namespace NConsole.Options.Registration.KeyValue
{
    using Xunit.Abstractions;

    public class StringIntegerKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<string, int>
    {
        public StringIntegerKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
