using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    internal class GuidOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<Guid>
    {
        protected override IEnumerable<string> RenderValue(Guid value) => GetRange($"{value:N}");

        protected override IEnumerable<Guid> GetNominalValueRange()
        {
            yield return Guid.NewGuid();
            yield return Guid.NewGuid();
        }

        protected override IEnumerable<object[]> RenderAllArguments(IEnumerable<string> prototypeNames
            , string prefix, string currentPrototype, char? requiredOrOptional, Guid value)
        {
            // ReSharper disable PossibleMultipleEnumeration
            var expectedNames = prototypeNames.Where(x => DoesPrototypeContainName(currentPrototype, x)).ToArray();
            var unexpectedNames = prototypeNames.Where(x => !DoesPrototypeContainName(currentPrototype, x)).ToArray();

            foreach (var callback in RenderCaseCallbacks)
            {
                var args = prototypeNames.SelectMany(p => callback(prefix, p, requiredOrOptional, value)).ToArray();
                // ReSharper disable once ImplicitlyCapturedClosure
                var expectedValues = expectedNames.Select(_ => value).ToArray();
                var unprocessedArgs = unexpectedNames.SelectMany(p => callback(prefix, p, requiredOrOptional, value)).ToArray();

                yield return GetRangeArray<object>(args, expectedValues, unprocessedArgs);
            }
            // ReSharper restore PossibleMultipleEnumeration
        }

        protected override IEnumerable<RenderPrototypeCasesDelegate<Guid>> RenderCaseCallbacks
        {
            get
            {
                IEnumerable<string> RenderBaseCase(string prefix, string prototypeName, char? requiredOrOptional, Guid value)
                {
                    yield return $"{prefix}{prototypeName}{(requiredOrOptional.HasValue ? $"{requiredOrOptional.Value}" : "")}{RenderValue(value).Single()}";
                }

                IEnumerable<string> RenderExtendedCase(string prefix, string prototypeName, char? requiredOrOptional, Guid value)
                {
                    yield return $"{prefix}{prototypeName}";
                    yield return $"{RenderValue(value).Single()}";
                }

                yield return RenderBaseCase;
                yield return RenderExtendedCase;
            }
        }
    }
}
