using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    using static Characters;
    using static TestFixtureBase;

    internal class RequiredOrOptionalOptionSetRegistrationTestCases : OptionSetRegistrationTestCases
    {
        private static IEnumerable<object[]> _privateCases;

        protected override IEnumerable<object[]> Cases
            => _privateCases ?? (_privateCases
                   = MergeCases(base.Cases, GetRange<object>(Equal, Colon).ToArray())
               );
    }
}
