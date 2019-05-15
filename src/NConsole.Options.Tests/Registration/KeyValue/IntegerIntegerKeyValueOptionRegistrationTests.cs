namespace NConsole.Options.Registration.KeyValue
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
