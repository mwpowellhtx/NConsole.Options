using Xunit.Abstractions;

namespace NConsole.Options.Registration.Targets
{
    public class DoubleTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<double>
    {
        public DoubleTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}