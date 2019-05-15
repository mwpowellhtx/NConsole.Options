using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Registration
{
    using static Characters;
    using static TestFixtureBase;

    internal class OptionSetRegistrationTestCases : TestCasesBase
    {
        private static IEnumerable<char?> _requiredOrOptionalCases;

        /// <summary>
        /// Gets the Required or Optional Cases.
        /// </summary>
        /// <see cref="Colon"/>
        /// <see cref="Equal"/>
        protected static IEnumerable<char?> RequiredOrOptionalCases
        {
            get
            {
                IEnumerable<char?> GetAll(char required, char optional)
                {
                    yield return null;
                    yield return required;
                    yield return optional;
                }

                return _requiredOrOptionalCases ?? (_requiredOrOptionalCases
                           = GetAll(Equal, Colon).ToArray()
                       );
            }
        }

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

                return _privateCases ?? (_privateCases = GetAll(Nominal));
            }
        }
    }
}
