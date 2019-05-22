using System;
using System.Collections.Generic;

namespace NConsole.Options
{
    internal static class CollectionExtensionMethods
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            // ReSharper disable PossibleMultipleEnumeration
            foreach (var value in values)
            {
                action.Invoke(value);
            }

            return values;
            // ReSharper restore PossibleMultipleEnumeration
        }
    }
}
