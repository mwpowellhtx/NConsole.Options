//using System;

//namespace NConsole.Options
//{
//    using Xunit;
//    using Xunit.Abstractions;
//    using static String;

//    // TODO: TBD: this one is perhaps closer to the typed version, excepting for the string-based GetOptions override...
//    public class StringActionOptionSetTests : TypedActionOptionSetTestFixtureBase<string>
//    {
//        protected override string DefaultValue => Empty;

//        public StringActionOptionSetTests(ITestOutputHelper outputHelper)
//            : base(outputHelper)
//        {
//        }

//#pragma warning disable xUnit1008
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="prototype"></param>
//        /// <param name="args"></param>
//        /// <param name="expectedVal"></param>
//        /// <param name="expectedUnprocessed"></param>
//        [ClassData(typeof(StringActionOptionSetTestCases))]
//        public override void VerifyVisitedOptions(string prototype, string[] args, string expectedVal, string[] expectedUnprocessed)
//            => base.VerifyVisitedOptions(prototype, args, expectedVal, expectedUnprocessed);
//#pragma warning restore xUnit1008

//    }
//}
