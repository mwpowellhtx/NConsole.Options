using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing
{
    using Registration;
    using static Characters;
    using static Domain;

    internal abstract class OptionSetParsingTestCases : OptionSetRegistrationTestCases
    {
        private static IEnumerable<string> _unbundledArgumentPrefixes;

        protected static IEnumerable<string> UnbundledArgumentPrefixes
        {
            get
            {
                IEnumerable<string> GetAll()
                {
                    yield return $"{Slash}";
                    yield return DoubleDash;
                }

                return _unbundledArgumentPrefixes ?? (_unbundledArgumentPrefixes = GetAll().ToArray());
            }
        }

        private static IEnumerable<string> _bundledArgumentPrefixes;

        protected static IEnumerable<string> BundledArgumentPrefixes
        {
            get
            {
                IEnumerable<string> GetAll()
                {
                    yield return $"{Dash}";
                }

                return _bundledArgumentPrefixes ?? (_bundledArgumentPrefixes = GetAll().ToArray());
            }
        }
    }
}
