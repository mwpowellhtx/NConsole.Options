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
        /// &quot;&lt;&gt;&quot;
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

        /// <summary>
        /// &quot;+-&quot;
        /// </summary>
        /// <see cref="Plus"/>
        /// <see cref="Dash"/>
        public static readonly string BooleanFlags = $"{Plus}{Dash}";

        /// <summary>
        /// &quot;,&quot;
        /// </summary>
        /// <see cref="Comma"/>
        public static readonly string DefaultSeparators = $"{Comma}";

        /// <summary>
        /// Gets the DefaultSeparator.
        /// </summary>
        /// <see cref="Comma"/>
        public static char DefaultSeparator => Comma;
    }
}
