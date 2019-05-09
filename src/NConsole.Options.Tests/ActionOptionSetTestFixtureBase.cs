namespace NConsole.Options
{
    using Xunit.Abstractions;

    public abstract class ActionOptionSetTestFixtureBase<T> : OptionSetTestFixtureBase
    {
        protected ActionOptionSetTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
