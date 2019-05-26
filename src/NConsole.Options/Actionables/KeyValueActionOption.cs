namespace NConsole.Options
{
    /// <summary>
    /// Represents a <typeparamref name="TKey"/> and <typeparamref name="TValue"/> based Option
    /// asset.
    /// </summary>
    /// <inheritdoc cref="InvokableActionOption{TCallback}"/>
    internal sealed class KeyValueActionOption<TKey, TValue> : InvokableActionOption<OptionCallback<TKey, TValue>>, IKeyValueActionOption
    {
        /// <summary>
        /// Gets the MaximumParameterCount, 2.
        /// </summary>
        /// <inheritdoc />
        internal override int MaximumParameterCount { get; } = 2;

        /// <inheritdoc />
        internal KeyValueActionOption(string prototype, string description, OptionCallback<TKey, TValue> callback)
            : base(prototype, description, callback)
        {
        }

        /// <inheritdoc />
        protected override void OnVisitation(OptionContext context) => Callback.Invoke(
            Parse<TKey>(context.Parameters[0], context)
            , Parse<TValue>(context.Parameters[1], context)
        );
    }
}
