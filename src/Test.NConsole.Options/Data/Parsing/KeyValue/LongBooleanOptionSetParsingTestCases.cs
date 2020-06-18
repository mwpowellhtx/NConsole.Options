using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.KeyValue
{
    internal class LongBooleanOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<long, bool>
    {
        protected override IEnumerable<long> NominalPairKeys => ProtectedLongIntegers;

        protected override IEnumerable<bool> NominalPairValues => ProtectedBooleans;

        protected override string RenderKey(long key) => RenderLongInteger(key);

        protected override string RenderValue(bool value) => RenderBoolean(value);
    }
}
