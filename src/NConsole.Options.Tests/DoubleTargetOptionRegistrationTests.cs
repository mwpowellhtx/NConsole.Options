using Xunit.Abstractions;

namespace NConsole.Options
{
    public class DoubleTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<double>
    {
        public DoubleTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}