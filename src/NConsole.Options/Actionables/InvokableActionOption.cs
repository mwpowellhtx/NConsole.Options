using System;

namespace NConsole.Options
{
    internal abstract class InvokableActionOption<TCallback> : Option
        where TCallback : Delegate
    {
        /// <summary>
        /// Provides <see cref="Delegate"/> Callback capability for any Action oriented
        /// <see cref="Option"/>.
        /// </summary>
        protected TCallback Callback { get; }

        /// <summary>
        /// Protected Constructor.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <inheritdoc />
        protected InvokableActionOption(string prototype, string description, TCallback callback)
            : base(prototype, description)
        {
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }
    }
}
