using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing
{
    using Registration;

    internal abstract class RequiredOrOptionalOptionSetParsingTestCasesBase : OptionSetParsingTestCases
    {
        private static IEnumerable<object[]> _privateCases;

        /// <summary>
        /// Gets the Cases. Merges the Base Cases with non-null
        /// <see cref="OptionSetRegistrationTestCases.RequiredOrOptionalCases"/>.
        /// </summary>
        protected override IEnumerable<object[]> Cases
            => _privateCases ?? (_privateCases = MergeCases(
                   base.Cases
                   , RequiredOrOptionalCases.Where(x => x.HasValue).Select(x => (object) x)
               ));

    }
}
