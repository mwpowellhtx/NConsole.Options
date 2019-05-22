//using System.Collections.Generic;
//using System.Linq;

//namespace NConsole.Options
//{
//    using Data;
//    using static Characters;
//    using static TestFixtureBase;

//    internal abstract class TypedAlphabetSoupTestCasesBase<T> : AlphabetSoupTestCasesBase
//    {
//        protected abstract string DefaultValueString { get; }

//        protected abstract IEnumerable<T> TypedValues { get; }

//        protected abstract string RenderTypedValue(T value);

//        private IEnumerable<object[]> _privateCases;

//        protected override IEnumerable<object[]> Cases
//        {
//            get
//            {
//                IEnumerable<object[]> GetAll(char delimiter)
//                {
//                    var defaultVal = DefaultValueString;

//                    var nominal = AlphabetOptionNames.Take(Nominal).ToArray();

//                    var emptyRange = GetRange<string>().ToArray();

//                    foreach (var x in TypedValues)
//                    {
//                        for (var i = 0; i < nominal.Length; ++i)
//                        {
//                            var expectedVal = RenderTypedValue(x);

//                            var parts = nominal[i].Split(Pipe);

//                            var dashShort = $"{Dash}{parts[0]}{delimiter}{expectedVal}";
//                            var dashLong = $"{Dash}{parts[1]}{delimiter}{expectedVal}";

//                            var dashDashShort = $"{Dash}{Dash}{parts[0]}{delimiter}{expectedVal}";
//                            var dashDashLong = $"{Dash}{Dash}{parts[1]}{delimiter}{expectedVal}";

//                            var slashShort = $"{Slash}{parts[0]}{delimiter}{expectedVal}";
//                            var slashLong = $"{Slash}{parts[1]}{delimiter}{expectedVal}";

//                            foreach (var count in GetRange(1, 3))
//                            {
//                                var dashShortRange = GetRepeatedRange(dashShort, count).ToArray();
//                                var dashDashShortRange = GetRepeatedRange(dashDashShort, count).ToArray();
//                                var slashShortRange = GetRepeatedRange(slashShort, count).ToArray();

//                                var dashLongRange = GetRepeatedRange(dashLong, count).ToArray();
//                                var dashDashLongRange = GetRepeatedRange(dashDashLong, count).ToArray();
//                                var slashLongRange = GetRepeatedRange(slashLong, count).ToArray();

//                                switch (i)
//                                {
//                                    case 0:
//                                        yield return GetRange<object>(dashShortRange, expectedVal, emptyRange).ToArray();
//                                        yield return GetRange<object>(dashDashShortRange, expectedVal, emptyRange).ToArray();
//                                        yield return GetRange<object>(dashLongRange, expectedVal, emptyRange).ToArray();
//                                        yield return GetRange<object>(dashDashLongRange, expectedVal, emptyRange).ToArray();
//                                        yield return GetRange<object>(slashShortRange, expectedVal, emptyRange).ToArray();
//                                        yield return GetRange<object>(slashLongRange, expectedVal, emptyRange).ToArray();
//                                        break;

//                                    default:
//                                        yield return GetRange<object>(dashShortRange, defaultVal, dashShortRange).ToArray();
//                                        yield return GetRange<object>(dashDashShortRange, defaultVal, dashDashShortRange).ToArray();
//                                        yield return GetRange<object>(dashLongRange, defaultVal, dashLongRange).ToArray();
//                                        yield return GetRange<object>(dashDashLongRange, defaultVal, dashDashLongRange).ToArray();
//                                        yield return GetRange<object>(slashShortRange, expectedVal, slashShortRange).ToArray();
//                                        yield return GetRange<object>(slashLongRange, expectedVal, slashLongRange).ToArray();
//                                        break;
//                                }
//                            }
//                        }
//                    }
//                }

//                return _privateCases ?? (_privateCases = MergeCases(
//                           AdaptBaseCases(Equal).Concat(AdaptBaseCases(Colon))
//                           , GetAll(Equal).Concat(GetAll(Colon))
//                       ));
//            }
//        }
//    }
//}
