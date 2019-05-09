using System.Collections.Generic;

namespace NConsole.Options
{
    internal class BooleanActionOptionSetTestCases : TypedAlphabetSoupTestCasesBase<bool>
    {
        protected override string DefaultValueString => "false";

        protected override IEnumerable<bool> TypedValues
        {
            get
            {
                yield return false;
                yield return true;
            }
        }

        protected override string RenderTypedValue(bool value) => $"{value}".ToLower();
    }
}
