using System;
using System.Collections.Generic;

namespace NConsole.Options
{
    /// <inheritdoc />
    public class UnprocessedRequiredOptionsException : Exception
    {
        /// <summary>
        /// Gets the list of Unprocessed <see cref="IOption"/> instances.
        /// </summary>
        public IReadOnlyList<IOption> UnprocessedOptions { get; }

        /// <summary>
        /// Internal <paramref name="unprocessedOptions"/> Constructor.
        /// </summary>
        /// <param name="unprocessedOptions"></param>
        /// <inheritdoc />
        internal UnprocessedRequiredOptionsException(params IOption[] unprocessedOptions)
        {
            UnprocessedOptions = unprocessedOptions;
        }

        /// <summary>
        /// Internal <paramref name="unprocessedOptions"/> Constructor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="unprocessedOptions"></param>
        /// <inheritdoc />
        internal UnprocessedRequiredOptionsException(string message, params IOption[] unprocessedOptions)
            : base(message)
        {
            UnprocessedOptions = unprocessedOptions;
        }
    }
}
