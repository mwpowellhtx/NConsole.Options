using System;
using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.KeyValue
{
    // TODO: TBD: I think it would also be interesting to support the boolean [+-] cases as well...
    /// <summary>
    /// 
    /// </summary>
    internal class GuidBooleanOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<Guid, bool>
    {
        protected override IEnumerable<Guid> NominalPairKeys => ProtectedUniversalUniqueIdentifiers;

        protected override IEnumerable<bool> NominalPairValues => ProtectedBooleans;

        protected override string RenderKey(Guid key) => RenderUniversalUniqueIdentifier(key);

        protected override string RenderValue(bool value) => RenderBoolean(value);
    }
}
