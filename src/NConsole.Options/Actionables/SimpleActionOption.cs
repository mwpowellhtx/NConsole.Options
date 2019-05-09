namespace NConsole.Options
{
    /// <summary>
    /// Represents an <see cref="OptionValueCollection"/> based <see cref="Option"/> asset.
    /// </summary>
    /// <inheritdoc />
    internal sealed class SimpleActionOption : InvokableActionOption<OptionCallback>
    {
        /// <inheritdoc />
        public SimpleActionOption(string prototype, string description, OptionCallback callback)
            : base(prototype, description, 0, callback)
        {
        }

        /// <inheritdoc />
        protected override void OnInvocation(OptionContext context) => Callback.Invoke();
    }
}
