using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChainingAssertion
{
    public static partial class AssertExtensions
    {
        /// <summary>verifies that <paramref name="actual"/> is equal to <paramref name="expected"/></summary>
        public static async Task Is<T>(this Task<T> actual, T expected, string message = "")
            => (await actual.ConfigureAwait(false)).Is(expected, message);

        /// <summary>verifies that the <paramref name="predicate"/> returns true</summary>
        public static async Task Is<T>(this Task<T> value, Expression<Func<T, bool>> predicate, string message = "")
            => (await value.ConfigureAwait(false)).Is(predicate, message);

        /// <summary>verifies that <paramref name="actual"/> is sequentially equal to <paramref name="expected"/></summary>
        public static async Task Is<T>(this Task<IEnumerable<T>> actual, params T[] expected)
            => (await actual.ConfigureAwait(false)).Is(expected);

        /// <summary>verifies that <paramref name="actual"/> is sequentially equal to <paramref name="expected"/></summary>
        public static async Task Is<T>(this Task<IEnumerable<T>> actual, IEnumerable<T> expected, string message = "")
            => (await actual.ConfigureAwait(false)).Is(expected, message);

        /// <summary>verifies that <paramref name="actual"/> is sequentially equal to <paramref name="expected"/></summary>
        public static async Task Is<T>(this Task<IEnumerable<T>> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer, string message = "")
            => (await actual.ConfigureAwait(false)).Is(expected, comparer, message);

        /// <summary>verifies that <paramref name="actual"/> is sequentially equal to <paramref name="expected"/></summary>
        public static async Task Is<T>(this Task<IEnumerable<T>> actual, IEnumerable<T> expected, Func<T, T, bool> equals, string message = "")
            => (await actual.ConfigureAwait(false)).Is(expected, equals, message);

        /// <summary>verifies that <paramref name="actual"/> is not equal to <paramref name="expected"/></summary>
        public static async Task IsNot<T>(this Task<T> actual, T expected, string message = "")
            => (await actual.ConfigureAwait(false)).IsNot(expected, message);

        /// <summary>verifies that <paramref name="actual"/> is not sequentially equal to <paramref name="expected"/></summary>
        public static async Task IsNot<T>(this Task<IEnumerable<T>> actual, params T[] expected)
            => (await actual.ConfigureAwait(false)).IsNot(expected);

        /// <summary>verifies that <paramref name="actual"/> is not sequentially equal to <paramref name="expected"/></summary>
        public static async Task IsNot<T>(this Task<IEnumerable<T>> actual, IEnumerable<T> expected, string message = "")
            => (await actual.ConfigureAwait(false)).IsNot(expected, message);

        /// <summary>verifies that <paramref name="actual"/> is not sequentially equal to <paramref name="expected"/></summary>
        public static async Task IsNot<T>(this Task<IEnumerable<T>> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer, string message = "")
            => (await actual.ConfigureAwait(false)).IsNot(expected, comparer, message);

        /// <summary>verifies that <paramref name="actual"/> is not sequentially equal to <paramref name="expected"/></summary>
        public static async Task IsNot<T>(this Task<IEnumerable<T>> actual, IEnumerable<T> expected, Func<T, T, bool> equals, string message = "")
            => (await actual.ConfigureAwait(false)).IsNot(expected, equals, message);

        /// <summary>verifies that the <paramref name="value"/> is null</summary>
        public static async Task IsNull<T>(this Task<T> value, string message = "")
            => (await value.ConfigureAwait(false)).IsNull(message);

        /// <summary>verifies that the <paramref name="value"/> is not null</summary>
        public static async Task IsNotNull<T>(this Task<T> value, string message = "")
            => (await value.ConfigureAwait(false)).IsNotNull(message);

        /// <summary>verifies that the <paramref name="value"/> is true</summary>
        public static async Task IsTrue(this Task<bool> value, string message = "")
            => (await value.ConfigureAwait(false)).IsTrue(message);

        /// <summary>verifies that the <paramref name="value"/> is false</summary>
        public static async Task IsFalse(this Task<bool> value, string message = "")
            => (await value.ConfigureAwait(false)).IsFalse(message);

        /// <summary>verifies that <paramref name="actual"/> is same reference as <paramref name="expected"/></summary>
        public static async Task IsSameReferenceAs<T>(this Task<T> actual, T expected, string message = "")
            => (await actual.ConfigureAwait(false)).IsSameReferenceAs(expected, message);

        /// <summary>verifies that <paramref name="actual"/> is not the same reference as <paramref name="expected"/></summary>
        public static async Task IsNotSameReferenceAs<T>(this Task<T> actual, T expected, string message = "")
            => (await actual.ConfigureAwait(false)).IsNotSameReferenceAs(expected, message);

        /// <summary>verifies that the <paramref name="value"/> is instance of type <typeparamref name="T"/></summary>
        public static async Task<T> IsInstanceOf<T>(this Task<object> value, string message = "")
            => (await value.ConfigureAwait(false)).IsInstanceOf<T>(message);

        /// <summary>verifies that the <paramref name="value"/> is not instance of type <typeparamref name="T"/></summary>
        public static async Task IsNotInstanceOf<T>(this Task<object> value, string message = "")
            => (await value.ConfigureAwait(false)).IsNotInstanceOf<T>(message);

        /// <summary>verifies that the <paramref name="collection"/> is empty</summary>
        public static async Task IsEmpty<T>(this Task<IEnumerable<T>> collection, string message = "")
            => (await collection.ConfigureAwait(false)).IsEmpty(message);

        /// <summary>verifies that the <paramref name="collection"/> is not empty</summary>
        public static async Task IsNotEmpty<T>(this Task<IEnumerable<T>> collection, string message = "")
            => (await collection.ConfigureAwait(false)).IsNotEmpty(message);
    }
}
