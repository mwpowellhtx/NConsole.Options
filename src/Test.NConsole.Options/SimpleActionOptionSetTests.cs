//namespace NConsole.Options
//{
//    using Xunit;
//    using Xunit.Abstractions;

//    public class SimpleActionOptionSetTests : OptionSetTestFixtureBase<string>
//    {
//        public SimpleActionOptionSetTests(ITestOutputHelper outputHelper)
//            : base(outputHelper)
//        {
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="prototype"></param>
//        /// <param name="args"></param>
//        /// <param name="expectedCount"></param>
//        /// <param name="expectedUnprocessed"></param>
//        [Theory, ClassData(typeof(SimpleActionOptionSetTestCases))]
//        public void VerifyVisitedOptions(string prototype, string[] args, int expectedCount, string[] expectedUnprocessed)
//        {
//            var count = 0;

//            var options = RegisterOptions(o =>
//            {
//                OptionsVisited[prototype] = count;
//                o.Add(prototype, () => OptionsVisited[prototype] = ++count);
//            });

//            options.Parse(args).ToArray().AssertNotNull().AssertEqual(expectedUnprocessed);

//            Assert.True(OptionsVisited.TryGetValue(prototype, out var actualCount));
//            Assert.Equal(expectedCount, Assert.IsType<int>(actualCount));
//        }
//    }
//}
