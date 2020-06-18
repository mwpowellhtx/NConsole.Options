namespace NConsole.Options.Registration.Targets
{
    using Xunit.Abstractions;

    public class FloatTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<float>
    {
        public FloatTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
