namespace NConsole.Options.Registration.Targets
{
    using Xunit.Abstractions;

    public class BooleanTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<bool>
    {
        public BooleanTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
