using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing
{
    using Registration;
    using static Characters;
    using static Domain;
    using static String;
    using static TestFixtureBase;
    using static StringComparison;
    using static StringSplitOptions;

    internal abstract class OptionSetParsingTestCasesBase : OptionSetRegistrationTestCases
    {
        protected static string RenderRequiredOrOptional(char? value) => value.HasValue ? $"{value.Value}" : "";

        protected static string RenderParts(params string[] parts) => Join(DefaultSeparator, parts);

        protected static char RenderBooleanShorthand(bool value) => value ? Plus : Dash;

        protected static string RenderCharacter(char value) => $"{value}";

        protected static string RenderInteger(int value) => $"{value}";

        protected static string RenderLongInteger(long value) => $"{value}";

        protected static string RenderUniversalUniqueIdentifier(Guid value) => $"{value:N}";

        protected static string RenderBoolean(bool value) => $"{value}".ToLower();

        private static IEnumerable<int> GetIntegerRange()
        {
            var value = 0;
            yield return ++value;
            yield return ++value;
            yield return ++value;
        }

        private static IEnumerable<long> GetLongIntegerRange()
        {
            var value = 0L;
            yield return ++value;
            yield return ++value;
            yield return ++value;
        }

        private static IEnumerable<char> GetCharRange()
        {
            var value = 'a';
            --value;
            yield return ++value;
            yield return ++value;
            yield return ++value;
        }

        private static IEnumerable<bool> GetBooleanRange()
        {
            yield return true;
            yield return false;
        }

        /// <summary>
        /// Returns a set of deterministic <see cref="Guid"/>.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Guid> GetUniversalUniqueIdentifierRange()
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var x in GetRange(
                "ae5d58c1-11cd-4d4f-8c22-158424943aa0"
                , "e511e193-7f1f-4689-b6b7-b934c61fa90b"
                , "723b0388-0c2a-48ac-b2dd-380b8cbf7e54"))
            {
                yield return Guid.Parse(x);
            }
        }

        private static Lazy<IEnumerable<int>> PrivateLazyIntegers { get; }
            = new Lazy<IEnumerable<int>>(GetIntegerRange);

        private static Lazy<IEnumerable<long>> PrivateLazyLongIntegers { get; }
            = new Lazy<IEnumerable<long>>(GetLongIntegerRange);

        private static Lazy<IEnumerable<char>> PrivateLazyCharacters { get; }
            = new Lazy<IEnumerable<char>>(GetCharRange);

        private static Lazy<IEnumerable<bool>> PrivateLazyBooleans { get; }
            = new Lazy<IEnumerable<bool>>(GetBooleanRange);

        private static Lazy<IEnumerable<Guid>> PrivateLazyUniversalUniqueIdentifiers { get; }
            = new Lazy<IEnumerable<Guid>>(GetUniversalUniqueIdentifierRange);

        protected static IEnumerable<int> ProtectedIntegers => PrivateLazyIntegers.Value;

        protected static IEnumerable<long> ProtectedLongIntegers => PrivateLazyLongIntegers.Value;

        protected static IEnumerable<char> ProtectedCharacters => PrivateLazyCharacters.Value;

        protected static IEnumerable<bool> ProtectedBooleans => PrivateLazyBooleans.Value;

        protected static IEnumerable<Guid> ProtectedUniversalUniqueIdentifiers
            => PrivateLazyUniversalUniqueIdentifiers.Value;

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
