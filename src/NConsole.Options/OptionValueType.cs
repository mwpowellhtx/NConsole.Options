namespace NConsole.Options
{
    /// <summary>
    /// Indicates the disposition towards the <see cref="Option"/>.
    /// </summary>
    public enum OptionValueType
    {
        /// <summary>
        /// There is No discernible Value Type.
        /// </summary>
        None,

        /// <summary>
        /// The Value is Optional.
        /// </summary>
        Optional,

        /// <summary>
        /// The Value is Required.
        /// </summary>
        Required
    }
}
