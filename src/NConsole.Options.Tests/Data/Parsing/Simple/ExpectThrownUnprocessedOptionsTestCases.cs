using System;
using System.Collections.Generic;
using System.Linq;
using Kingdom.Combinatorics.Combinatorials;

namespace NConsole.Options.Data.Parsing
{
    using static Characters;
    using static Domain;
    using static String;
    using static TestFixtureBase;

    internal class ExpectThrownUnprocessedOptionsTestCases : OptionSetParsingTestCasesBase
    {
        private static IEnumerable<object[]> _privateCases;

        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetAll()
                {
                    // Should also inform Bundled arguments.
                    var argPrototypes = PrototypeNames.Select(x => GetRangeArray($"{x[0]}", x)).ToArray();

                    for (var i = 0; i < argPrototypes.Length; ++i)
                    {
                        var specPrototypes = argPrototypes.Take(i).ToArray();
                        var actualPrototypes = specPrototypes.Select(x => $"{Join(Pipe, x)}{Equal}").ToArray();

                        // This is a bit of a long winded Combination, but we basically want to iterate over the short- and long-hand Names.
                        var combinations = new Combiner(specPrototypes.Select(x => x.Select(y => (object) y)).ToArray()) {SilentOverflow = true};

                        // Iterate over these combinations then dissect based on parsed and thrown expectations.
                        foreach (var current in combinations)
                        {
                            for (var j = 0; j < i; ++j)
                            {
                                yield return GetRangeArray<object>(
                                    actualPrototypes
                                    , current.Take(j).Select(x => $"{DoubleDash}{x}").ToArray()
                                    , actualPrototypes.Skip(j).ToArray()
                                );
                            }
                        }
                    }

                    // TODO: TBD: also consider how to specify bundled options...
                }

                return _privateCases ?? (_privateCases = GetAll().ToArray());
            }
        }
    }
}
