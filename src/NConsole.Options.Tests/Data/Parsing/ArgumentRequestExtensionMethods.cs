using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data
{
    using static Characters;
    using static String;
    using static ArgumentRequest;

    internal static class ArgumentRequestExtensionMethods
    {
        public static bool ContainsAny(this ArgumentRequest value, params ArgumentRequest[] values)
            => values.Any(x => x.ContainedBy(value));

        public static bool ContainsAll(this ArgumentRequest value, params ArgumentRequest[] values)
            => values.Aggregate(None, (g, x) => g | x).ContainedBy(value);

        public static bool ContainedBy(this ArgumentRequest value, ArgumentRequest mask) => (mask & value) != None;

        /// <summary>
        /// Returns the Joined <paramref name="args"/> using the <paramref name="separator"/>.
        /// Default Separator is the <see cref="Pipe"/>.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        /// <see cref="Pipe"/>
        /// <see cref="Join(string, IEnumerable{string})"/>
        public static string ToPipedArguments(this IEnumerable<string> args, char separator = Pipe)
            => Join($"{separator}", args);

        public static IEnumerable<string> GetRequestedArguments(this string name, ArgumentRequest request)
        {
            if (request.ContainsAny(Letter))
            {
                yield return $"{name[0]}";
            }

            if (request.ContainsAny(Full))
            {
                yield return $"{name}";
            }
        }
    }
}
