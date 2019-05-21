namespace NConsole.Options.Registration.Targets
{
    using Xunit.Abstractions;

    public class DoubleTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<double>
    {
        public DoubleTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
