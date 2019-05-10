namespace NConsole.Options
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
