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

        protected InvokableActionOption(string prototype, string description, TCallback callback)
            : this(prototype, description, 0, callback)
        {
        }

        protected InvokableActionOption(string prototype, string description, int count, TCallback callback)
            : base(prototype, description, count)
        {
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }
    }
}
