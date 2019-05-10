using Xunit.Abstractions;

namespace NConsole.Options
{
    public class LongTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<long>
    {
        public LongTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}