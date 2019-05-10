namespace NConsole.Options
{
    /// <summary>
    /// Represents a <typeparamref name="TKey"/> and <typeparamref name="TValue"/> based Option
    /// asset.
    /// </summary>
    /// <inheritdoc />
    internal sealed class KeyValueActionOption<TKey, TValue> : InvokableActionOption<OptionCallback<TKey, TValue>>
    {
        /// <inheritdoc />
        public KeyValueActionOption(string prototype, string description, OptionCallback<TKey, TValue> callback)
            : base(prototype, description, 2, callback)
        {
        }

        /// <inheritdoc />
        protected override void OnVisitation(OptionContext context) => Callback.Invoke(
            Parse<TKey>(context.OptionValues[0], context)
            , Parse<TValue>(context.OptionValues[1], context)
        );
    }
}
