using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    using static Characters;
    using static TestFixtureBase;
    using static String;

    internal class StringActionOptionSetTestCases : AlphabetSoupTestCasesBase
    {
        private IEnumerable<object[]> _privateCases;

        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetAll(char delimiter)
                {
                    var nominal = AlphabetOptionNames.Take(Nominal).ToArray();

                    var emptyRange = GetRange<string>().ToArray();

                    for (var i = 0; i < nominal.Length; ++i)
                    {
                        var guid = Guid.NewGuid();
                        var expectedGuid = $"{guid:N}";

                        int FilterCount(int x) => i == 0 ? x : 0;

                        var parts = nominal[i].Split(Pipe);

                        var dashShort = $"{Dash}{parts[0]}{delimiter}{expectedGuid}";
                        var dashLong = $"{Dash}{parts[1]}{delimiter}{expectedGuid}";

                        var dashDashShort = $"{Dash}{Dash}{parts[0]}{delimiter}{expectedGuid}";
                        var dashDashLong = $"{Dash}{Dash}{parts[1]}{delimiter}{expectedGuid}";

                        foreach (var count in GetRange(1, 3))
                        {
                            var dashShortRange = GetRepeatedRange(dashShort, count).ToArray();
                            var dashDashShortRange = GetRepeatedRange(dashDashShort, count).ToArray();

                            var dashLongRange = GetRepeatedRange(dashLong, count).ToArray();
                            var dashDashLongRange = GetRepeatedRange(dashDashLong, count).ToArray();

                            switch (i)
                            {
                                case 0:
                                    yield return GetRange<object>(dashShortRange, expectedGuid, emptyRange).ToArray();
                                    yield return GetRange<object>(dashDashShortRange, expectedGuid, emptyRange).ToArray();
                                    yield return GetRange<object>(dashLongRange, expectedGuid, emptyRange).ToArray();
                                    yield return GetRange<object>(dashDashLongRange, expectedGuid, emptyRange).ToArray();
                                    break;

                                default:
                                    yield return GetRange<object>(dashShortRange, Empty, dashShortRange).ToArray();
                                    yield return GetRange<object>(dashDashShortRange, Empty, dashDashShortRange).ToArray();
                                    yield return GetRange<object>(dashLongRange, Empty, dashLongRange).ToArray();
                                    yield return GetRange<object>(dashDashLongRange, Empty, dashDashLongRange).ToArray();
                                    break;
                            }
                        }
                    }
                }

                // Adapts the Base Cases assuming a Delimiter.
                IEnumerable<object[]> AdaptBaseCases(char delimiter)
                {
                    return base.Cases.Select(x => GetRange<object>($"{x[0]}{delimiter}").ToArray());
                }

                return _privateCases ?? (_privateCases = MergeCases(
                           AdaptBaseCases(Equal).Concat(AdaptBaseCases(Colon))
                           , GetAll(Equal).Concat(GetAll(Colon))
                       ));
            }
        }
    }
}
