using System;
using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    internal class GuidOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<Guid>
    {
        protected override IEnumerable<string> RenderValue(Guid value) => GetRange($"{value:N}");

        protected override IEnumerable<Guid> NominalValues => ProtectedUniversalUniqueIdentifiers;
    }
}
