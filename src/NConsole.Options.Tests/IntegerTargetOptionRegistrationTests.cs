using Xunit.Abstractions;

namespace NConsole.Options
{
    public class IntegerTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<int>
    {
        public IntegerTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}