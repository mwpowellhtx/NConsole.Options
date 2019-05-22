using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    internal static class ReflectionExtensionMethods
    {
        /// <summary>
        /// The default Pass Through Callback simply accepts the Generic Argument <see cref="Type"/>.
        /// </summary>
        private static VerifyOptionGenericArgumentCallback PassThroughCallback => t => true;

        public static bool TryVerifyKeyValueActionOptionTypes<T>(this T option
            , VerifyOptionGenericArgumentCallback keyTypeCallback = null
            , VerifyOptionGenericArgumentCallback valueTypeCallback = null)
            where T : IKeyValueActionOption
        {
            var optionType = option.GetType();
            var callbacks = new[] {keyTypeCallback, valueTypeCallback};

            // ReSharper disable PossibleMultipleEnumeration
            bool TryVerifyGenericType(IEnumerable<Type> types)
                => types.Count() == callbacks.Length
                   && types.Zip(callbacks, (x, callback) => (callback ?? PassThroughCallback).Invoke(x)).All(z => z);
            // ReSharper restore PossibleMultipleEnumeration

            return optionType.IsGenericType && TryVerifyGenericType(optionType.GetGenericArguments());
        }
    }
}
