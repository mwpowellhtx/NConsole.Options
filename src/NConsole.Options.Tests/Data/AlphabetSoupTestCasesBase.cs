using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data
{
    using static Characters;
    using static TestFixtureBase;

    internal abstract class AlphabetSoupTestCasesBase : TestCasesBase
    {
        private static IEnumerable<string> _alphabetOptionNames;

        internal static IEnumerable<string> AlphabetOptionNames
            => _alphabetOptionNames ?? (_alphabetOptionNames
                   = AlphabetInstance.Select(x => x.ToLower())
                       .Select(x => $"{x[0]}{Pipe}{x}").ToArray()
               );

        private IEnumerable<object[]> _privateCases;

        /// <summary>
        /// Gets the <see cref="Pipe"/> formatted Option Names as the root Dimension.
        /// </summary>
        private IEnumerable<object[]> PrivateCases
            => _privateCases ?? (_privateCases
                   = AlphabetOptionNames
                       .Take(One)
                       .Select(x => GetRange<object>(x).ToArray()).ToArray()
               );

        /// <summary>
        /// Gets the <see cref="Pipe"/> formatted Option Names as the root Dimension.
        /// </summary>
        protected override IEnumerable<object[]> Cases => PrivateCases;

        // Adapts the Base Cases assuming a Delimiter.
        protected IEnumerable<object[]> AdaptBaseCases(char delimiter) => PrivateCases.Select(
            x => GetRange<object>($"{x[0]}{delimiter}").ToArray()
        );
    }
}
