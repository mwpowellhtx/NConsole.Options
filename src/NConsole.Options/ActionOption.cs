using System;

namespace NConsole.Options
{
    internal abstract class ActionOptionBase<T> : Option
    {
        private readonly OptionCallback<T> _callback;

        protected ActionOptionBase(string prototype, string description, int count, OptionCallback<T> callback)
            : base(prototype, description, count)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        protected override void OnParsed(OptionContext c) => _callback(
            Parse<T>(c.OptionValues[0], c)
        );
    }

    internal sealed class ActionOption : ActionOptionBase<OptionValueCollection>
    {
        public ActionOption(string prototype, string description, int count, OptionCallback<OptionValueCollection> callback)
            : base(prototype, description, count, callback)
        {
        }
    }

    internal sealed class ActionOption<T> : ActionOptionBase<T>
    {
        public ActionOption(string prototype, string description, OptionCallback<T> callback)
            : base(prototype, description, 1, callback)
        {
        }
    }

    internal sealed class ActionOption<TKey, TValue> : Option
    {
        private readonly OptionCallback<TKey, TValue> _callback;

        public ActionOption(string prototype, string description, OptionCallback<TKey, TValue> callback)
            : base(prototype, description, 2)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        protected override void OnParsed(OptionContext context) => _callback(
            Parse<TKey>(context.OptionValues[0], context)
            , Parse<TValue>(context.OptionValues[1], context)
        );
    }
}
