using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static ChainingAssertion.AssertionService;

namespace ChainingAssertion
{
    public static partial class AssertExtensions
    {
        /// <summary>verifies that <paramref name="actual"/> is equal to <paramref name="expected"/></summary>
        public static void Is<T>(this T actual, T expected, string message = "")
        {
            if (typeof(T) != typeof(string) && actual is IEnumerable eActual && expected is IEnumerable eExpected)
                Assertion.Equal(eExpected.Cast<object>(), eActual.Cast<object>(), null, message);
            else
                Assertion.Equal(expected, actual, message);
        }

        /// <summary>verifies that the <paramref name="predicate"/> returns true</summary>
        public static void Is<T>(this T value, Expression<Func<T, bool>> predicate, string message = "")
        {
            if (predicate.Compile().Invoke(value))
                return;

            var dump = ExpressionDumper.Dump(predicate, value);
            var members = string.Join(", ", dump.Select(x => x.Key + " = " + x.Value));
            var additional = (string.IsNullOrEmpty(message)) ? string.Empty : "\n" + message;

            Assertion.Fail($"\n{members}\n{predicate}{additional}");
        }

        /// <summary>verifies that <paramref name="actual"/> is sequencially equal to <paramref name="expected"/></summary>
        public static void Is<T>(this IEnumerable<T> actual, params T[] expected)
            => actual.Is(expected.AsEnumerable());

        /// <summary>verifies that <paramref name="actual"/> is sequencially equal to <paramref name="expected"/></summary>
        public static void Is<T>(this IEnumerable<T> actual, IEnumerable<T> expected, string message = "")
            => Assertion.Equal(expected, actual, null, message);

        /// <summary>verifies that <paramref name="actual"/> is sequencially equal to <paramref name="expected"/></summary>
        public static void Is<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer, string message = "")
            => Assertion.Equal(expected, actual, comparer, message);

        /// <summary>verifies that <paramref name="actual"/> is sequencially equal to <paramref name="expected"/></summary>
        public static void Is<T>(this IEnumerable<T> actual, IEnumerable<T> expected, Func<T, T, bool> equals, string message = "")
            => Assertion.Equal(expected, actual, new AssertEqualityComparer<T>(equals), message);

        /// <summary>verifies that <paramref name="actual"/> is not equal to <paramref name="expected"/></summary>
        public static void IsNot<T>(this T actual, T expected, string message = "")
        {
            if (typeof(T) != typeof(string) && actual is IEnumerable eActual && expected is IEnumerable eExpected)
                Assertion.NotEqual(eExpected.Cast<object>(), eActual.Cast<object>(), message);
            else
                Assertion.NotEqual(expected, actual, message);
        }

        /// <summary>verifies that <paramref name="actual"/> is not sequencially equal to <paramref name="expected"/></summary>
        public static void IsNot<T>(this IEnumerable<T> actual, params T[] expected)
            => actual.IsNot(expected.AsEnumerable());

        /// <summary>verifies that <paramref name="actual"/> is not sequencially equal to <paramref name="expected"/></summary>
        public static void IsNot<T>(this IEnumerable<T> actual, IEnumerable<T> expected, string message = "")
            => Assertion.NotEqual(expected, actual, message);

        /// <summary>verifies that <paramref name="actual"/> is not sequencially equal to <paramref name="expected"/></summary>
        public static void IsNot<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer, string message = "")
            => Assertion.NotEqual(expected, actual, comparer, message);

        /// <summary>verifies that <paramref name="actual"/> is not sequencially equal to <paramref name="expected"/></summary>
        public static void IsNot<T>(this IEnumerable<T> actual, IEnumerable<T> expected, Func<T, T, bool> equals, string message = "")
            => Assertion.NotEqual(expected, actual, new AssertEqualityComparer<T>(equals), message);

        /// <summary>verifies that the <paramref name="value"/> is null</summary>
        public static void IsNull<T>(this T value, string message = "")
            => Assertion.Null(value, message);

        /// <summary>verifies that the <paramref name="value"/> is not null</summary>
        public static void IsNotNull<T>(this T value, string message = "")
            => Assertion.NotNull(value, message);

        /// <summary>verifies that the <paramref name="value"/> is true</summary>
        public static void IsTrue(this bool value, string message = "")
            => Assertion.True(value, message);

        /// <summary>verifies that the <paramref name="value"/> is false</summary>
        public static void IsFalse(this bool value, string message = "")
            => Assertion.False(value, message);

        /// <summary>verifies that <paramref name="actual"/> is same reference as <paramref name="expected"/></summary>
        public static void IsSameReferenceAs<T>(this T actual, T expected, string message = "")
            => Assertion.ReferenceEqual(expected, actual, message);

        /// <summary>verifies that <paramref name="actual"/> is not the same reference as <paramref name="expected"/></summary>
        public static void IsNotSameReferenceAs<T>(this T actual, T expected, string message = "")
            => Assertion.NotReferenceEqual(expected, actual, message);

        /// <summary>verifies that the <paramref name="value"/> is instance of type <typeparamref name="T"/></summary>
        public static T IsInstanceOf<T>(this object value, string message = "")
        {
            Assertion.InstanceOf<T>(value, message);
            return (T)value;
        }

        /// <summary>verifies that the <paramref name="value"/> is not instance of type <typeparamref name="T"/></summary>
        public static void IsNotInstanceOf<T>(this object value, string message = "")
            => Assertion.NotInstanceOf<T>(value, message);

        /// <summary>verifies that the <paramref name="collection"/> is empty</summary>
        public static void IsEmpty<T>(this IEnumerable<T> collection, string message = "")
            => collection.Any().IsFalse(message);

        /// <summary>verifies that the <paramref name="collection"/> is not empty</summary>
        public static void IsNotEmpty<T>(this IEnumerable<T> collection, string message = "")
            => collection.Any().IsTrue(message);
    }
}
