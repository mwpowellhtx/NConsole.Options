using Xunit.Abstractions;

namespace NConsole.Options
{
    public class BooleanTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<bool>
    {
        public BooleanTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}