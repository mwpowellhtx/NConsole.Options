using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    using static Characters;
    using static TestFixtureBase;

    internal class OptionSetRegistrationTestCases : TestCasesBase
    {
        /// <summary>
        /// Gets the <see cref="MilitaryAlphabet.Instance"/>.
        /// </summary>
        private static MilitaryAlphabet AlphabetInstance => MilitaryAlphabet.Instance;

        private static IEnumerable<object[]> _privateCases;

        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetAll(int count)
                {
                    foreach (var name in AlphabetInstance.Take(count).Select(x => x.ToLower()))
                    {
                        var prototype = $"{name[0]}{Pipe}{name}";
                        var description = $"The {name} option";
                        yield return GetRange<object>(prototype, description).ToArray();
                    }
                }

                const int nominal = 3;

                return _privateCases ?? (_privateCases = GetAll(nominal));
            }
        }
    }
}
