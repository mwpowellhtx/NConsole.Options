namespace NConsole.Options
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
