namespace NConsole.Options.Registration.KeyValue
{
    using Xunit.Abstractions;

    public class StringDoubleKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<string, double>
    {
        public StringDoubleKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
