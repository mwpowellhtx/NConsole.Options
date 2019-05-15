using System.Linq;
using System.Text.RegularExpressions;

namespace NConsole.Options
{
    /// <summary>
    /// Provides several useful <see cref="char"/> based constant assets.
    /// </summary>
    internal static class RegexExtensionMethods
    {
        public static string GetGroupValueOrDefault(this Match match, string groupName, string defaultValue = null)
            => match.IsGroupSuccessful(groupName) ? match.Groups[groupName].Value : defaultValue;

        public static string GetGroupValue(this Match match, string groupName)
            => match.Groups[groupName].Value;

        public static bool IsGroupSuccessful(this Match match, string groupName)
            => match.Groups[groupName].Success;

        public static bool AreGroupsSuccessful(this Match match, params string[] groupNames)
            => groupNames.All(match.IsGroupSuccessful);
    }
}
