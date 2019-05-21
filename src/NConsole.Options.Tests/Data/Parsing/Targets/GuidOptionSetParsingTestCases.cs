using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    internal class GuidOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<Guid>
    {
        protected override IEnumerable<string> RenderValue(Guid value) => GetRange($"{value:N}");

        protected override IEnumerable<Guid> NominalValues => ProtectedUniversalUniqueIdentifiers;

        protected override IEnumerable<RenderPrototypeCasesDelegate<Guid>> RenderCaseCallbacks
        {
            get
            {
                IEnumerable<string> RenderBaseCase(string prefix, string prototypeName, char? requiredOrOptional, Guid value)
                {
                    yield return $"{prefix}{prototypeName}{(requiredOrOptional.HasValue ? $"{requiredOrOptional.Value}" : "")}{RenderValue(value).Single()}";
                }

                IEnumerable<string> RenderExtendedCase(string prefix, string prototypeName, char? requiredOrOptional, Guid value)
                {
                    yield return $"{prefix}{prototypeName}";
                    yield return $"{RenderValue(value).Single()}";
                }

                yield return RenderBaseCase;
                yield return RenderExtendedCase;
            }
        }
    }
}
