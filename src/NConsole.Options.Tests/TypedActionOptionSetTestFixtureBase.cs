namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class TypedActionOptionSetTestFixtureBase<T> : ActionOptionSetTestFixtureBase<T>
    {
        protected override OptionSet GetOptions()
        {
            var prototype = Prototype;
            OptionsVisited[prototype] = default(T);
            return new OptionSet {{prototype, (T x) => OptionsVisited[prototype] = x}};
        }

        protected TypedActionOptionSetTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1003
        [Theory]
        public virtual void VerifyVisitedOptions(string prototype, string[] args, T expectedVal, string[] expectedUnprocessed)
        {
            Prototype = prototype;

            var actualUnprocessed = Options.Parse(args).ToArray();

            Assert.NotNull(actualUnprocessed);
            Assert.Equal(expectedUnprocessed, actualUnprocessed);

            Assert.True(OptionsVisited.TryGetValue(prototype, out var actualVal));
            Assert.Equal(expectedVal, Assert.IsType<T>(actualVal));
        }
#pragma warning restore xUnit1003

    }
}
