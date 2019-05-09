namespace NConsole.Options
{
    /// <summary>
    /// Represents a <typeparamref name="TTarget"/> specific <see cref="Option"/> asset.
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <inheritdoc />
    internal abstract class ActionOptionBase<TTarget> : InvokableActionOption<OptionCallback<TTarget>>
    {
        /// <summary>
        /// Protected Constructor.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="count"></param>
        /// <param name="callback"></param>
        /// <inheritdoc />
        protected ActionOptionBase(string prototype, string description, int count, OptionCallback<TTarget> callback)
            : base(prototype, description, count, callback)
        {
        }

        /// <inheritdoc />
        protected override void OnInvocation(OptionContext context) => Callback.Invoke(
            Parse<TTarget>(context.OptionValues[0], context)
        );
    }
}
