using System.Collections.Generic;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    internal class BooleanOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<bool>
    {
        protected override IEnumerable<string> RenderValue(bool value) => GetRange($"{value}".ToLower());

        protected override IEnumerable<bool> NominalValues => ProtectedBooleans;

        protected override IEnumerable<RenderPrototypeCasesDelegate<bool>> RenderCaseCallbacks
        {
            get
            {
                foreach (var callback in base.RenderCaseCallbacks)
                {
                    yield return callback;
                }

                IEnumerable<string> RenderShorthand(string prefix, string prototypeName, char? requiredOrOptional, bool value)
                {
                    yield return $"{prefix}{prototypeName}{RenderBooleanShorthand(value)}";
                }

                yield return RenderShorthand;
            }
        }
    }
}
