using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    using Data;
    using static Characters;
    using static TestFixtureBase;

    internal class SimpleActionOptionSetTestCases : AlphabetSoupTestCasesBase
    {
        private IEnumerable<object[]> _privateCases;

        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetAll()
                {
                    var nominal = AlphabetOptionNames.Take(Nominal).ToArray();

                    var emptyRange = GetRange<string>().ToArray();

                    for (var i = 0; i < nominal.Length; ++i)
                    {
                        int FilterCount(int x) => i == 0 ? x : 0;

                        var parts = nominal[i].Split(Pipe);

                        var dashShort = $"{Dash}{parts[0]}";
                        var dashLong = $"{Dash}{parts[1]}";

                        var dashDashShort = $"{Dash}{Dash}{parts[0]}";
                        var dashDashLong = $"{Dash}{Dash}{parts[1]}";

                        var slashShort = $"{Slash}{parts[0]}";
                        var slashLong = $"{Slash}{parts[1]}";

                        foreach (var count in GetRange(1, 3))
                        {
                            var dashShortRange = GetRepeatedRange(dashShort, count).ToArray();
                            var dashDashShortRange = GetRepeatedRange(dashDashShort, count).ToArray();
                            var slashShortRange = GetRepeatedRange(slashShort, count).ToArray();

                            var dashLongRange = GetRepeatedRange(dashLong, count).ToArray();
                            var dashDashLongRange = GetRepeatedRange(dashDashLong, count).ToArray();
                            var slashLongRange = GetRepeatedRange(slashLong, count).ToArray();

                            var filteredCount = FilterCount(count);

                            switch (i)
                            {
                                case 0:
                                    yield return GetRange<object>(dashShortRange, filteredCount, emptyRange).ToArray();
                                    yield return GetRange<object>(dashDashShortRange, filteredCount, emptyRange).ToArray();
                                    yield return GetRange<object>(dashLongRange, filteredCount, emptyRange).ToArray();
                                    yield return GetRange<object>(dashDashLongRange, filteredCount, emptyRange).ToArray();
                                    yield return GetRange<object>(slashShortRange, filteredCount, emptyRange).ToArray();
                                    yield return GetRange<object>(slashLongRange, filteredCount, emptyRange).ToArray();
                                    break;

                                default:
                                    yield return GetRange<object>(dashShortRange, filteredCount, dashShortRange).ToArray();
                                    yield return GetRange<object>(dashDashShortRange, filteredCount, dashDashShortRange).ToArray();
                                    yield return GetRange<object>(dashLongRange, filteredCount, dashLongRange).ToArray();
                                    yield return GetRange<object>(dashDashLongRange, filteredCount, dashDashLongRange).ToArray();
                                    yield return GetRange<object>(slashShortRange, filteredCount, slashShortRange).ToArray();
                                    yield return GetRange<object>(slashLongRange, filteredCount, slashLongRange).ToArray();
                                    break;
                            }
                        }
                    }
                }

                return _privateCases ?? (_privateCases = MergeCases(base.Cases, GetAll()));
            }
        }
    }
}
