using Xunit.Abstractions;

namespace NConsole.Options
{
    public class StringTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<string>
    {
        public StringTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}