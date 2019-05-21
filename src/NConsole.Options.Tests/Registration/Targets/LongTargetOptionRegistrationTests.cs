namespace NConsole.Options.Registration.Targets
{
    using Xunit.Abstractions;

    public class LongTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<long>
    {
        public LongTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
