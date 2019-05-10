namespace NConsole.Options
{
    using Xunit.Abstractions;

    public class IntegerIntegerKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<int, int>
    {
       public IntegerIntegerKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
