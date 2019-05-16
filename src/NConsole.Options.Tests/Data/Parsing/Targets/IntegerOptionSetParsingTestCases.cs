using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.Targets
{
    internal class IntegerOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<int>
    {
        protected override string RenderValue(int value) => $"{value}";

        protected override IEnumerable<int> GetNominalValueRange()
        {
            for (int i = 0, x = 0; i < 7; ++i)
            {
                yield return x = x * 10 + i + 1;
            }
        }
    }
}
