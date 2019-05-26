namespace NConsole.Options
{
    /// <summary>
    /// Represents an <see cref="OptionValueCollection"/> based <see cref="Option"/> asset.
    /// </summary>
    /// <inheritdoc cref="InvokableActionOption{TCallback}"/>
    internal sealed class SimpleActionOption : InvokableActionOption<OptionCallback>, ISimpleActionOption
    {
        /// <summary>
        /// Gets the MaximumParameterCount, 0.
        /// </summary>
        internal override int MaximumParameterCount { get; } = 0;

        // TODO: TBD: public? or internal?
        /// <inheritdoc />
        internal SimpleActionOption(string prototype, string description, OptionCallback callback)
            : base(prototype, description, callback)
        {
        }

        /// <inheritdoc />
        protected override void OnVisitation(OptionContext context) => Callback.Invoke();
    }
}
