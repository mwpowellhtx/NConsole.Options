using System;
using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.KeyValue
{
    internal class UniversalUniqueIdentifierIntegerOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<Guid, int>
    {
        protected override IEnumerable<Guid> NominalPairKeys => ProtectedUniversalUniqueIdentifiers;

        protected override IEnumerable<int> NominalPairValues => ProtectedIntegers;

        protected override string RenderKey(Guid key) => RenderUniversalUniqueIdentifier(key);

        protected override string RenderValue(int value) => RenderInteger(value);
    }
}
