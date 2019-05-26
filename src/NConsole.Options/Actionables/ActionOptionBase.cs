namespace NConsole.Options
{
    /// <summary>
    /// Represents a <typeparamref name="TTarget"/> specific <see cref="Option"/> asset.
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <inheritdoc cref="InvokableActionOption{TCallback}"/>
    internal abstract class ActionOptionBase<TTarget> : InvokableActionOption<OptionCallback<TTarget>>, IInvokableActionOption
    {
        /// <summary>
        /// Protected Constructor.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <inheritdoc />
        protected ActionOptionBase(string prototype, string description, OptionCallback<TTarget> callback)
            : base(prototype, description, callback)
        {
        }

        /// <inheritdoc />
        protected override void OnVisitation(OptionContext context) => Callback.Invoke(
            Parse<TTarget>(context.Parameters[0], context)
        );
    }
}
