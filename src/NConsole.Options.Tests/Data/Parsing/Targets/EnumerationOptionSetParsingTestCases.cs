using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    /// <summary>
    /// We could use any Ordinal based Enumeration for this purpose, but we will use
    /// <see cref="StringComparison"/> for test purposes, as it is pretty much stock,
    /// and has a sufficiently nominal range of values to make testing worthwhile.
    /// </summary>
    internal class EnumerationOptionSetParsingTestCases
        : RequiredOrOptionalOptionSetParsingTestCasesBase<StringComparison>
    {
        protected override IEnumerable<string> RenderValue(StringComparison value) => GetRange(value.ToString());

        protected override IEnumerable<StringComparison> GetNominalValueRange()
        {
            foreach(StringComparison value in Enum.GetValues(typeof(StringComparison)))
            {
                yield return value;
            }
        }

        protected override IEnumerable<object[]> RenderAllArguments(IEnumerable<string> prototypeNames
            , string prefix, string currentPrototype, char? requiredOrOptional, StringComparison value)
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

        protected override IEnumerable<RenderPrototypeCasesDelegate<StringComparison>> RenderCaseCallbacks
        {
            get
            {
                IEnumerable<string> RenderBaseCase(string prefix, string prototypeName, char? requiredOrOptional, StringComparison value)
                {
                    yield return $"{prefix}{prototypeName}{(requiredOrOptional.HasValue ? $"{requiredOrOptional.Value}" : "")}{RenderValue(value).Single()}";
                }

                IEnumerable<string> RenderExtendedCase(string prefix, string prototypeName, char? requiredOrOptional, StringComparison value)
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
