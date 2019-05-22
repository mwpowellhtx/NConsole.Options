//using System.Collections.Generic;

//namespace NConsole.Options
//{
//    using Xunit;
//    using Xunit.Abstractions;

//    public abstract class TypedActionOptionSetTestFixtureBase<T> : ActionOptionSetTestFixtureBase<T>
//    {
//        /// <summary>
//        /// Override in order to specialize the desired sort of DefaultValue.
//        /// </summary>
//        protected virtual T DefaultValue => default(T);

//        protected TypedActionOptionSetTestFixtureBase(ITestOutputHelper outputHelper)
//            : base(outputHelper)
//        {
//        }

//#pragma warning disable xUnit1003
//        [Theory]
//        public virtual void VerifyVisitedOptions(string prototype, string[] args, T expectedVal, string[] expectedUnprocessed)
//        {
//            var options = RegisterOptions(o =>
//            {
//                //OptionsVisited[prototype] = DefaultValue;
//                o.Add(prototype, (T x) => OptionsVisited[prototype] = x);
//            });

//            options.Parse(args).ToArray().AssertNotNull().AssertEqual(expectedUnprocessed);

//            // ReSharper disable once ImplicitlyCapturedClosure
//            OptionsVisited.AssertOnTried(
//                (IDictionary<string, object> d, out object x) =>
//                {
//                    var tried = d.TryGetValue(prototype, out var y);
//                    x = tried ? y : null;
//                    return tried;
//                }
//                , x => x.AssertIsType<T>().AssertEqual(expectedVal));
//        }
//#pragma warning restore xUnit1003

//    }
//}
