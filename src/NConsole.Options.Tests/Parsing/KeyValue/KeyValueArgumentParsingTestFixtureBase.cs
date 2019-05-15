namespace NConsole.Options.Parsing.KeyValue
{
    using Xunit;
    using Xunit.Abstractions;
    using NConsole.Options.Registration.KeyValue;

    public abstract class KeyValueArgumentParsingTestFixtureBase<TKey, TValue> : KeyValueOptionRegistrationTestFixtureBase<TKey, TValue>
    {
        protected KeyValueArgumentParsingTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
