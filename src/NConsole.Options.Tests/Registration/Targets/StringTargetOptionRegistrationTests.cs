using Xunit.Abstractions;

namespace NConsole.Options.Registration.Targets
{
    public class StringTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<string>
    {
        public StringTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}