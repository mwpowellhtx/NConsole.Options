namespace NConsole.Options
{
    using static Characters;
    using static OptionValueType;

    internal static class OptionValueTypeExtensionMethods
    {
        /// <summary>
        /// Returns the optional, from a language perspective, <see cref="OptionValueType"/>
        /// result based on the input <paramref name="s"/>.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static OptionValueType? ToOptionValueType(this string s)
        {
            // Whatever else it may be, make sure we at least have a value.
            s = (s ?? "").Trim();

            // ReSharper disable SwitchStatementMissingSomeCases
            switch (s.Length)
            {
                case 1:
                    switch (s[0])
                    {
                        case Colon: return Optional;
                        case Equal: return Required;
                    }

                    break;
            }
            // ReSharper restore SwitchStatementMissingSomeCases

            return null;
        }
    }
}
