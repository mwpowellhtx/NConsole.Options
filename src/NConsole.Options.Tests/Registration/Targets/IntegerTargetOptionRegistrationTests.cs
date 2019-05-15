using Xunit.Abstractions;

namespace NConsole.Options.Registration.Targets
{
    public class IntegerTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<int>
    {
        public IntegerTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}