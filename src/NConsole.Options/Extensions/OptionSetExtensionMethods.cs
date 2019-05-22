using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    public static class OptionSetExtensionMethods
    {
        /// <summary>
        /// Tries to <see cref="OptionSet.Parse"/> the <paramref name="args"/>, receiving the
        /// <paramref name="unprocessedArgs"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="args"></param>
        /// <param name="unprocessedArgs"></param>
        /// <returns></returns>
        public static bool TryParse(this OptionSet options, IEnumerable<string> args, out IEnumerable<string> unprocessedArgs)
        {
            // ReSharper disable PossibleMultipleEnumeration
            unprocessedArgs = options.Parse(args.ToArray());
            return unprocessedArgs.Count() != args.Count();
            // ReSharper restore PossibleMultipleEnumeration
        }

        /// <summary>
        /// Tries to <see cref="OptionSet.Parse"/> the <paramref name="args"/>. Any unprocessed
        /// <paramref name="args"/> are effectively ignored and discarded.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool TryParse(this OptionSet options, params string[] args) => options.TryParse(args, out _);
    }
}
