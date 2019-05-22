using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.KeyValue
{
    // TODO: TBD: I think it would also be interesting to support the boolean [+-] cases as well...
    /// <summary>
    /// 
    /// </summary>
    internal class UniversalUniqueIdentifierBooleanOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase<Guid, bool>
    {
        protected override IEnumerable<Guid> NominalPairKeys => ProtectedUniversalUniqueIdentifiers;

        protected override IEnumerable<bool> NominalPairValues => ProtectedBooleans;

        protected override string RenderKey(Guid key) => RenderUniversalUniqueIdentifier(key);

        protected override string RenderValue(bool value) => RenderBoolean(value);

        protected override IEnumerable<RenderPrototypeCasesDelegate<KeyValuePair<Guid, bool>>> RenderCaseCallbacks
        {
            get
            {
                foreach (var callback in base.RenderCaseCallbacks)
                {
                    yield return callback;
                }

                // TODO: TBD: may be able to provide these from a utilities class, or perhaps from the base class.
                // TODO: TBD: the tricky part is that rendering gets domain specific and verbose very quickly...
                IEnumerable<string> RenderFullyBundledShorthand(string prefix, string prototypeName, char? requiredOrOptional, KeyValuePair<Guid, bool> pair)
                {
                    var (key, value) = pair;
                    yield return $"{prefix}{prototypeName}{RenderRequiredOrOptional(requiredOrOptional)}{key:N}{RenderBooleanShorthand(value)}";
                }

                IEnumerable<string> RenderPartiallyBundledShorthand(string prefix, string prototypeName, char? requiredOrOptional, KeyValuePair<Guid, bool> pair)
                {
                    var (key, value) = pair;
                    yield return $"{prefix}{prototypeName}{RenderRequiredOrOptional(requiredOrOptional)}{RenderBooleanShorthand(value)}";
                    yield return $"{key:N}";
                }

                yield return RenderFullyBundledShorthand;
                yield return RenderPartiallyBundledShorthand;
            }
        }
    }
}
