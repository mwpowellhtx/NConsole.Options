using System;
using System.Collections;
using System.Collections.Generic;

namespace NConsole.Options
{
    using Xunit;

    // TODO: TBD: really, it would be interesting to compile a set of "fluent" extensions based on xUnit Assert.XYZ ...
    internal static class AssertExtensionMethods
    {
        // ReSharper disable ConditionIsAlwaysTrueOrFalse
        public static bool AssertTrue(this bool value)
        {
            Assert.True(value);
            return value;
        }

        public static bool? AssertTrue(this bool? value)
        {
            Assert.True(value);
            return value;
        }

        public static bool AssertFalse(this bool value)
        {
            Assert.False(value);
            return value;
        }

        public static bool? AssertFalse(this bool? value)
        {
            Assert.False(value);
            return value;
        }
        // ReSharper restore ConditionIsAlwaysTrueOrFalse

        public static T AssertNotNull<T>(this T value)
        {
            Assert.NotNull(value);
            return value;
        }

        public static T AssertNull<T>(this T value)
        {
            Assert.Null(value);
            // ReSharper disable once ExpressionIsAlwaysNull
            return value;
        }

        public static T AssertEmpty<T>(this T value)
            where T : IEnumerable
        {
            Assert.Empty(value);
            return value;
        }

        public static T AssertNotEmpty<T>(this T value)
            where T : IEnumerable
        {
            Assert.NotEmpty(value);
            return value;
        }

        public static T AssertEqual<T>(this T actual, T expected)
        {
            Assert.Equal(expected, actual);
            return expected;
        }

        public static T AssertEqual<T>(this T actual, T expected, IEqualityComparer<T> comparer)
        {
            Assert.Equal(expected, actual, comparer);
            return actual;
        }

        // ReSharper disable PossibleMultipleEnumeration
        public static IEnumerable<T> AssertCollection<T>(this IEnumerable<T> values, params Action<T>[] valueInspectors)
        {
            Assert.Collection(values, valueInspectors);
            return values;
        }
        // ReSharper restore PossibleMultipleEnumeration

        public static T AssertIsType<T>(this object obj) => Assert.IsType<T>(obj);

        // ReSharper disable PossibleMultipleEnumeration
        public static IEnumerable<T> AssertContains<T>(this IEnumerable<T> values, T value, BinaryPredicate<T> predicate = null)
        {
            predicate = predicate ?? ((x, y) => x.Equals(y));
            Assert.Contains(values, x => predicate(x, value));
            return values;
        }
        // ReSharper restore PossibleMultipleEnumeration

        public static T AssertContainedBy<T>(this T value, IEnumerable<T> values, BinaryPredicate<T> predicate = null)
        {
            AssertContains(values, value, predicate);
            return value;
        }

        public delegate bool TryGetCallback<in TTarget, T>(TTarget target, out T value);

        public delegate void OnTriedCallback<in T>(T value);

        public static TTarget AssertOnTried<TTarget, T>(this TTarget target, TryGetCallback<TTarget, T> tryCallback,
            OnTriedCallback<T> onTriedCallback)
        {
            if (tryCallback(target, out var result))
            {
                onTriedCallback(result);
            }

            return target;
        }

        public static TException AssertThrowsException<TException>(this Action action, Action<TException> response = null)
            where TException : Exception
        {
            var ex = Assert.Throws<TException>(action);
            response?.Invoke(ex);
            return ex;
        }
    }
}
