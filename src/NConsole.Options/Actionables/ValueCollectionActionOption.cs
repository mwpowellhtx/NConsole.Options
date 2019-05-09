namespace NConsole.Options
{
    /// <summary>
    /// Represents an <see cref="OptionValueCollection"/> based <see cref="Option"/> asset.
    /// </summary>
    /// <inheritdoc />
    internal sealed class ValueCollectionActionOption : ActionOptionBase<OptionValueCollection>
    {
        /// <inheritdoc />
        public ValueCollectionActionOption(string prototype, string description, int count, OptionCallback<OptionValueCollection> callback)
            : base(prototype, description, count, callback)
        {
        }

        // TODO: TBD: do we need to handle this one different during OnInvocation?
    }
}
