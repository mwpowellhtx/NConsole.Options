using Xunit.Abstractions;

namespace NConsole.Options.Registration.Targets
{
    public class BooleanTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<bool>
    {
        public BooleanTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}