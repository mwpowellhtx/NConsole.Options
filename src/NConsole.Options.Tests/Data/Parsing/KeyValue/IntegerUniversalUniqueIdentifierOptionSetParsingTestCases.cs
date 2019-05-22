using System;
using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.KeyValue
{
    internal class IntegerUniversalUniqueIdentifierOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<int, Guid>
    {
        protected override IEnumerable<int> NominalPairKeys => ProtectedIntegers;

        protected override IEnumerable<Guid> NominalPairValues => ProtectedUniversalUniqueIdentifiers;

        protected override string RenderKey(int key) => RenderInteger(key);

        protected override string RenderValue(Guid value) => RenderUniversalUniqueIdentifier(value);
    }
}
