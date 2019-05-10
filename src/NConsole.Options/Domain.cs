namespace NConsole.Options
{
    using static Characters;

    /// <summary>
    /// Provides several <see cref="string"/> or other based constant, or at least read-only,
    /// assets.
    /// </summary>
    internal static class Domain
    {
        /// <summary>
        /// &apos;&lt;&gt;&apos;
        /// </summary>
        public static string AngleBrackets = $"{AngleBracketOpen}{AngleBracketClose}";

        /// <summary>
        /// 0
        /// </summary>
        public const int Zed = 0;

        // ReSharper disable InconsistentNaming
        /// <summary>
        /// &quot;VALUE&quot;
        /// </summary>
        public const string VALUE = nameof(VALUE);

        /// <summary>
        /// &quot;or&quot;
        /// </summary>
        public const string or = nameof(or);
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// &quot;--&quot;
        /// </summary>
        /// <see cref="Dash"/>
        public static readonly string DoubleDash = $"{Dash}{Dash}";
    }
}
