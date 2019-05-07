namespace NConsole.Options
{
    using static Characters;

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

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// &quot;VALUE&quot;
        /// </summary>
        public const string VALUE = nameof(VALUE);

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// &quot;or&quot;
        /// </summary>
        public const string or = nameof(or);
    }
}
