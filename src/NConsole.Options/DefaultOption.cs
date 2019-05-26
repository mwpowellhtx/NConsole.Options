namespace NConsole.Options
{
    using static Domain;

    /// <inheritdoc cref="IDefaultOption"/>
    /// <see cref="Option"/>
    /// <see cref="IDefaultOption"/>
    internal class DefaultOption : InvokableActionOption<OptionCallback>, IDefaultOption
    {
        /// <summary>
        /// Gets the Instance.
        /// </summary>
        internal static DefaultOption Instance { get; } = new DefaultOption(() => { });

        /// <summary>
        /// The Default Option Should Not Report during the Written Report.
        /// </summary>
        /// <inheritdoc />
        internal override bool ShouldReport { get; } = false;

        /// <summary>
        /// Gets the Maximum Parameter Count, 0 for the Default.
        /// </summary>
        /// <inheritdoc />
        internal override int MaximumParameterCount { get; } = 0;

        /// <summary>
        /// Default Internal Constructor.
        /// </summary>
        /// <inheritdoc />
        private DefaultOption(OptionCallback callback)
            : base(AngleBrackets, "A default option placeholder.", callback)
        {
        }

        /// <summary>
        /// Visitation is a No-Op for purposes of the Default <see cref="Option"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        protected override void OnVisitation(OptionContext context) => Callback.Invoke();
    }
}
