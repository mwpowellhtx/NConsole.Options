namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;

    public class SimpleActionOptionSetTests : OptionSetTestFixtureBase
    {
        /// <summary>
        /// Returns the Simple Action Option Set.
        /// Will bump an internal Counter for every option that we encounter.
        /// </summary>
        /// <returns></returns>
        protected override OptionSet GetOptions()
        {
            var count = 0;
            var prototype = Prototype;
            OptionsVisited[prototype] = count;
            return new OptionSet {{prototype, () => OptionsVisited[prototype] = ++count}};
        }

        public SimpleActionOptionSetTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="args"></param>
        /// <param name="expectedCount"></param>
        /// <param name="expectedUnprocessed"></param>
        [Theory, ClassData(typeof(SimpleActionOptionSetTestCases))]
        public void VerifyVisitedOptions(string prototype, string[] args, int expectedCount, string[] expectedUnprocessed)
        {
            Assert.NotNull(prototype);
            Assert.NotEmpty(prototype);

            Prototype = prototype;

            var actualUnprocessed = Options.Parse(args).ToArray();

            Assert.NotNull(actualUnprocessed);
            Assert.Equal(expectedUnprocessed, actualUnprocessed);

            Assert.True(OptionsVisited.TryGetValue(prototype, out var actualCount));
            Assert.Equal(expectedCount, Assert.IsType<int>(actualCount));
        }
    }
}
