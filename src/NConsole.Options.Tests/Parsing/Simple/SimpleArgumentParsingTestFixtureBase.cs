namespace NConsole.Options.Parsing.Simple
{
    using Xunit;
    using Xunit.Abstractions;
    using NConsole.Options.Registration.Simple;

    public abstract class SimpleArgumentParsingTestFixtureBase : SimpleOptionRegistrationTestFixtureBase
    {
        protected SimpleArgumentParsingTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
