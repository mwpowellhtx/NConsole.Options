namespace NConsole.Options.Registration.Targets
{
    using Xunit.Abstractions;

    public class StringTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<string>
    {
        public StringTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
