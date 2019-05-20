using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.Targets
{
    using Kingdom.Combinatorics.Combinatorials;
    using static Characters;
    using static TestFixtureBase;

    internal class BooleanOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<bool>
    {
        protected override IEnumerable<string> RenderValue(bool value) => GetRange($"{value}".ToLower());

        protected override IEnumerable<bool> GetNominalValueRange()
        {
            yield return false;
            yield return true;
        }

        protected override IEnumerable<object[]> RenderAllArguments(IEnumerable<string> prototypeNames
            , string prefix, string currentPrototype, char? requiredOrOptional, bool value)
        {
            // ReSharper disable PossibleMultipleEnumeration
            var expectedNames = prototypeNames.Where(x => DoesPrototypeContainName(currentPrototype, x)).ToArray();
            var unexpectedNames = prototypeNames.Where(x => !DoesPrototypeContainName(currentPrototype, x)).ToArray();

            foreach (var callback in RenderCaseCallbacks.ToArray())
            {
                var args = prototypeNames.SelectMany(p => callback(prefix, p, requiredOrOptional, value)).ToArray();
                // ReSharper disable once ImplicitlyCapturedClosure
                var expectedValues = expectedNames.Select(_ => value).ToArray();
                var unprocessedArgs = unexpectedNames.SelectMany(p => callback(prefix, p, requiredOrOptional, value)).ToArray();

                yield return GetRangeArray<object>(args, expectedValues, unprocessedArgs);
            }
            // ReSharper restore PossibleMultipleEnumeration
        }

        protected override IEnumerable<RenderPrototypeCasesDelegate<bool>> RenderCaseCallbacks
        {
            get
            {
                IEnumerable<string> RenderBaseCase(string prefix, string prototypeName, char? requiredOrOptional, bool value)
                {
                    yield return $"{prefix}{prototypeName}{(requiredOrOptional.HasValue ? $"{requiredOrOptional.Value}" : "")}{RenderValue(value).Single()}"; 
                }

                IEnumerable<string> RenderExtendedCase(string prefix, string prototypeName, char? requiredOrOptional, bool value)
                {
                    yield return $"{prefix}{prototypeName}";
                    yield return $"{RenderValue(value).Single()}";
                }

                yield return RenderBaseCase;
                yield return RenderExtendedCase;
            }
        }

        private static IEnumerable<KeyValuePair<string, string>> _booleanArguments;

        private static IEnumerable<KeyValuePair<string, string>> BooleanArguments
        {
            get
            {
                IEnumerable<KeyValuePair<string, string>> GetAll()
                {
                    var combinations = PrototypeNames.ToArray<object>() // PrototypeName
                        .Combine(
                            UnbundledArgumentPrefixes.ToArray<object>() // Prefix
                            , GetRange<object>(Plus, Dash) // Plus/true, Dash/false
                        );

                    combinations.SilentOverflow = true;

                    foreach (var current in combinations)
                    {
                        var name = (string) current[0];
                        var prefix = (string) current[1];
                        var flag = (char) current[2];

                        yield return KeyValuePair.Create(name, $"{prefix}{name}{flag}");
                        // TODO: TBD: which is borderline also potentially a Bundled variation...
                        yield return KeyValuePair.Create($"{name[0]}", $"{prefix}{name[0]}{flag}");
                    }
                }

                return _booleanArguments ?? (_booleanArguments = GetAll().ToArray());
            }
        }

        // TODO: TBD: we also need to handle the boolean +- test cases...

        private static IEnumerable<object[]> _privateCases;

        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetBooleanFlagCases()
                {
                    var args = BooleanArguments;

                    foreach (var merged in MergeCases(ProtectedRootCases, RequiredOrOptionalCases
                        .Where(x => x != null).Select<char?, object>(x => x.Value)))
                    {
                        var currentPrototype = (string) merged[0];
                        var description = (string) merged[1];
                        var requiredOrOptional = (char) merged[2];

                        // ReSharper disable PossibleMultipleEnumeration
                        yield return GetRangeArray<object>(
                            currentPrototype
                            , description
                            , requiredOrOptional
                            , args.Select(x => x.Value).ToArray()
                            , args.Where(x => DoesPrototypeContainName(currentPrototype, x.Key))
                                .Select(x => x.Value.Last() == Plus).ToArray()
                            , args.Where(x => !DoesPrototypeContainName(currentPrototype, x.Key))
                                .Select(x => x.Value).ToArray()
                        );
                        // ReSharper restore PossibleMultipleEnumeration
                    }
                }

                return _privateCases ?? (_privateCases = base.Cases.Concat(GetBooleanFlagCases()).ToArray());
            }
        }
    }
}
