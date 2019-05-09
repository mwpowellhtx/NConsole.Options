namespace NConsole.Options
{
    /// <summary>
    /// Represents a <typeparamref name="TTarget"/> based Option asset.
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <inheritdoc />
    internal sealed class ActionOption<TTarget> : ActionOptionBase<TTarget>
    {
        /// <inheritdoc />
        public ActionOption(string prototype, string description, OptionCallback<TTarget> callback)
            : base(prototype, description, 1, callback)
        {
        }
    }
}
