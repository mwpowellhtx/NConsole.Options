namespace NConsole.Options.Registration.Targets
{
    using Xunit.Abstractions;

    public class IntegerTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<int>
    {
        public IntegerTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
