namespace NConsole.Options.Registration.KeyValue
{
    using Xunit.Abstractions;

    public class StringStringKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<string, string>
    {
        public StringStringKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
