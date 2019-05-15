using Xunit.Abstractions;

namespace NConsole.Options.Registration.Targets
{
    public class LongTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<long>
    {
        public LongTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}