using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing
{
    using Registration;
    using static Characters;
    using static Domain;
    using static StringComparison;
    using static StringSplitOptions;

    internal abstract class OptionSetParsingTestCases : OptionSetRegistrationTestCases
    {
        protected static bool DoesPrototypeContainName(string prototype, string name)
            => prototype.Split(Pipe, RemoveEmptyEntries)
                .Any(x => x.Equals(name, InvariantCultureIgnoreCase));

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
