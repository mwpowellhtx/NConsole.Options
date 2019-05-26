namespace NConsole.Options
{
    /// <summary>
    /// Represents a <typeparamref name="TTarget"/> based Option asset.
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <inheritdoc cref="ActionOptionBase{TTarget}"/>
    internal sealed class ActionOption<TTarget> : ActionOptionBase<TTarget>, IActionOption
    {
        /// <summary>
        /// Gets the MaximumParameterCount, 1.
        /// </summary>
        /// <inheritdoc />
        internal override int MaximumParameterCount { get; } = 1;

        /// <inheritdoc />
        internal ActionOption(string prototype, string description, OptionCallback<TTarget> callback)
            : base(prototype, description, callback)
        {
        }
    }
}
