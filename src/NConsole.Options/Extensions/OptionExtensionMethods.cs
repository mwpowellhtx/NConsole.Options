namespace NConsole.Options
{
    using static OptionValueType;

    /// <summary>
    /// Provides a set of helpful <see cref="IOption"/> extension methods.
    /// </summary>
    public static class OptionExtensionMethods
    {
        /// <summary>
        /// Returns whether <paramref name="option"/> Is <see cref="Required"/>.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public static bool IsRequired(this IOption option) => option?.ValueType == Required;
    }
}
