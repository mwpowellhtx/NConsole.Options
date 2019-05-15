using Xunit.Abstractions;

namespace NConsole.Options.Registration.Targets
{
    public class FloatTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<float>
    {
        public FloatTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}