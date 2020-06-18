namespace NConsole.Options.Registration.KeyValue
{
    using Xunit.Abstractions;

    public class StringBooleanKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<string, bool>
    {
        public StringBooleanKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
