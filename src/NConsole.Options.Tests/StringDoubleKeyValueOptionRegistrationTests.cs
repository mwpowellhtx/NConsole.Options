namespace NConsole.Options
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
