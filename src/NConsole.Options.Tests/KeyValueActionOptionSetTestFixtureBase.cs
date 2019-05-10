namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class KeyValueActionOptionSetTestFixtureBase<TKey, TValue> : OptionSetTestFixtureBase<TKey>
    {
        protected KeyValueActionOptionSetTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1003
        [Theory]
        public virtual void VerifyVisitedOptions(string prototype, string[] args, bool expected
            , TKey expectedKey, TValue expectedVal, string[] expectedUnprocessed)
        {
            var options = RegisterOptions(o =>
            {
                // This time the test method really does need to consider whether an option was set.
                o.Add(prototype, (TKey k, TValue v) => OptionsVisited[k] = v);
            });

            options.Parse(args).ToArray().AssertNotNull().AssertEqual(expectedUnprocessed);

            if (!OptionsVisited.TryGetValue(expectedKey, out var actualVal).AssertEqual(expected))
            {
                return;
            }

            Assert.Equal(expectedVal, Assert.IsType<TValue>(actualVal));
        }
#pragma warning restore xUnit1003

    }
}
