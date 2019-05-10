using Xunit.Abstractions;

namespace NConsole.Options
{
    public class FloatTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<float>
    {
        public FloatTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}