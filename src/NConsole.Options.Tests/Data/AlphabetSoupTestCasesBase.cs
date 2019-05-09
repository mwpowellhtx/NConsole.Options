using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    using static Characters;
    using static TestFixtureBase;

    internal abstract class AlphabetSoupTestCasesBase : TestCasesBase
    {
        /// <summary>
        /// 1
        /// </summary>
        protected const int One = 1;

        /// <summary>
        /// 3
        /// </summary>
        protected const int Nominal = 3;

        private static MilitaryAlphabet Alphabet { get; } = MilitaryAlphabet.Instance;

        private static IEnumerable<string> _alphabetOptionNames;

        internal static IEnumerable<string> AlphabetOptionNames
            => _alphabetOptionNames ?? (_alphabetOptionNames
                   = Alphabet.Select(x => x.ToLower())
                       .Select(x => $"{x[0]}{Pipe}{x}").ToArray()
               );

        private IEnumerable<object[]> _privateCases;

        /// <summary>
        /// Gets the <see cref="Pipe"/> formatted Option Names as the root Dimension.
        /// </summary>
        protected override IEnumerable<object[]> Cases
            => _privateCases ?? (_privateCases
                   = AlphabetOptionNames
                       .Take(One)
                       .Select(x => GetRange<object>(x).ToArray()).ToArray()
               );
    }
}
