using System;
using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.Targets
{
    internal class GuidOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<Guid>
    {
        protected override string RenderValue(Guid value) => $"{value:N}";

        protected override IEnumerable<Guid> GetNominalValueRange()
        {
            yield return Guid.NewGuid();
            yield return Guid.NewGuid();
        }
    }
}
