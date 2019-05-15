namespace NConsole.Options
{
    /// <summary>
    /// Represents a <typeparamref name="TTarget"/> based Option asset.
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <inheritdoc cref="ActionOptionBase{TTarget}"/>
    internal sealed class ActionOption<TTarget> : ActionOptionBase<TTarget>, IActionOption
    {
        // TODO: TBD: Public? or Internal?
        /// <inheritdoc />
        public ActionOption(string prototype, string description, OptionCallback<TTarget> callback)
            : base(prototype, description, 1, callback)
        {
        }
    }
}
