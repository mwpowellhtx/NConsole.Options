using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.KeyValue
{
    internal class IntegerCharOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<int, char>
    {
        protected override IEnumerable<int> NominalPairKeys => ProtectedIntegers;

        protected override IEnumerable<char> NominalPairValues => ProtectedCharacters;

        protected override string RenderKey(int key) => RenderInteger(key);

        protected override string RenderValue(char value) => RenderCharacter(value);
    }
}
