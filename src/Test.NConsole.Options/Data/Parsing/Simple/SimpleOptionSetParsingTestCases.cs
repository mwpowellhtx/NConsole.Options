using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing
{
    using static TestFixtureBase;

    internal class SimpleOptionSetParsingTestCases : OptionSetParsingTestCasesBase
    {
        private static IEnumerable<object[]> _privateCases;

        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetAll()
                {
                    IEnumerable<KeyValuePair<string, string>> GetArgumentPairs(string name)
                    {
                        foreach (var prefix in UnbundledArgumentPrefixes)
                        {
                            yield return KeyValuePair.Create(name, $"{prefix}{name}");
                        }
                    }

                    IEnumerable<object> GetOne(string prototype, string description, params string[] args)
                    {
                        yield return prototype;
                        yield return description;
                        var pairs = args.SelectMany(GetArgumentPairs).ToArray();
                        yield return pairs.Select(x => x.Value).ToArray();
                        // TODO: TBD: there are probably stronger ways to assert this level of success.
                        yield return pairs.Select(x => DoesPrototypeContainName(prototype, x.Key)).Where(x => x).ToArray();
                        yield return pairs.Where(x => !DoesPrototypeContainName(prototype, x.Key)).Select(x => x.Value).ToArray();
                    }

                    foreach (var @case in ProtectedRootCases)
                    {
                        var prototype = (string) @case[0];
                        var description = (string) @case[1];

                        yield return GetOne(prototype, description
                            , PrototypeNames.SelectMany(x => GetRange(x, $"{x[0]}")).ToArray()).ToArray();

                        foreach (var @base in PrototypeNames)
                        {
                            yield return GetOne(prototype, description
                                , GetRange($"{@base[0]}", @base).ToArray()).ToArray();
                        }
                    }
                }

                return _privateCases ?? (_privateCases = GetAll().ToArray());
            }
        }
    }
}
