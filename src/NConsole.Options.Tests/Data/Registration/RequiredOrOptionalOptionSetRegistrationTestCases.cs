using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Registration
{
    internal class RequiredOrOptionalOptionSetRegistrationTestCases : OptionSetRegistrationTestCases
    {
        private static IEnumerable<object[]> _privateCases;

        // TODO: TBD: add Separator test cases especially for KeyValue testing...
        // TODO: TBD: but may also need/want to intercept any exceptions thrown for Simple or even Target registration...
        protected override IEnumerable<object[]> Cases
            => _privateCases ?? (_privateCases
                   = MergeCases(base.Cases, RequiredOrOptionalCases.Select(x => (object) x))
               );
    }
}
