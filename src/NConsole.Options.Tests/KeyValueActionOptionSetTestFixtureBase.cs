namespace NConsole.Options
{
    using Xunit.Abstractions;

    public abstract class KeyValueActionOptionSetTestFixtureBase<TKey, TValue> : OptionSetTestFixtureBase
    {
        protected KeyValueActionOptionSetTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
