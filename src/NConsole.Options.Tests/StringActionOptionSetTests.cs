using System;

namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;
    using static String;

    public class StringActionOptionSetTests : ActionOptionSetTestFixtureBase<string>
    {
        protected override OptionSet GetOptions()
        {
            var prototype = Prototype;
            OptionsVisited[prototype] = Empty;
            return new OptionSet {{prototype, s => OptionsVisited[prototype] = s}};
        }

        public StringActionOptionSetTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        // TODO: TBD: could potentially capture an Expected Value generic type here, if it does not get too complicated to do so.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="args"></param>
        /// <param name="expectedVal"></param>
        /// <param name="expectedUnprocessed"></param>
        [Theory, ClassData(typeof(StringActionOptionSetTestCases))]
        public void VerifyVisitedOptions(string prototype, string[] args, string expectedVal, string[] expectedUnprocessed)
        {
            Prototype = prototype;

            var actualUnprocessed = Options.Parse(args).ToArray();

            Assert.NotNull(actualUnprocessed);
            Assert.Equal(expectedUnprocessed, actualUnprocessed);

            Assert.True(OptionsVisited.TryGetValue(prototype, out var actualVal));
            Assert.Equal(expectedVal, Assert.IsType<string>(actualVal));
        }
    }
}
