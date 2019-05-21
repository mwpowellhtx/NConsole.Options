using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    internal class IntegerOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<int>
    {
        protected override IEnumerable<string> RenderValue(int value) => GetRange($"{value}");

        protected override IEnumerable<int> NominalValues
        {
            get
            {
                for (int i = 0, x = 0; i < 7; ++i)
                {
                    yield return x = x * 10 + i + 1;
                }
            }
        }

        protected override IEnumerable<RenderPrototypeCasesDelegate<int>> RenderCaseCallbacks
        {
            get
            {
                IEnumerable<string> RenderBaseCase(string prefix, string prototypeName, char? requiredOrOptional, int value)
                {
                    yield return $"{prefix}{prototypeName}{(requiredOrOptional.HasValue ? $"{requiredOrOptional.Value}" : "")}{RenderValue(value).Single()}";
                }

                IEnumerable<string> RenderExtendedCase(string prefix, string prototypeName, char? requiredOrOptional, int value)
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
