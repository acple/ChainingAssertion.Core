using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainingAssertion
{
    internal partial class AssertionService : IAssertionService
    {
        public void Equal<T>(T expected, T actual, string message)
            => Assert.AreEqual(expected, actual, message);

        public void NotEqual<T>(T expected, T actual, string message)
            => Assert.AreNotEqual(expected, actual, message);

        public void Equal<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer, string message)
            => CollectionAssert.AreEqual(
                expected.ToArray(),
                actual.ToArray(),
                (comparer is AssertEqualityComparer<T> eq) ? eq : new AssertEqualityComparer<T>(comparer ?? EqualityComparer<T>.Default),
                message);

        public void NotEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer, string message)
            => CollectionAssert.AreNotEqual(
                expected.ToArray(),
                actual.ToArray(),
                (comparer is AssertEqualityComparer<T> eq) ? eq : new AssertEqualityComparer<T>(comparer ?? EqualityComparer<T>.Default),
                message);

        public void Null<T>(T value, string message)
            => Assert.IsNull(value, message);

        public void NotNull<T>(T value, string message)
            => Assert.IsNotNull(value, message);

        public void True(bool condition, string message)
            => Assert.IsTrue(condition, message);

        public void False(bool condition, string message)
            => Assert.IsFalse(condition, message);

        public void ReferenceEqual<T>(T expected, T actual, string message)
            => Assert.AreSame(expected, actual, message);

        public void NotReferenceEqual<T>(T expected, T actual, string message)
            => Assert.AreNotSame(expected, actual, message);

        public void InstanceOf<T>(object value, string message)
            => Assert.IsInstanceOfType(value, typeof(T), message);

        public void NotInstanceOf<T>(object value, string message)
            => Assert.IsNotInstanceOfType(value, typeof(T), message);

        public Exception Exception()
            => new AssertFailedException();

        public Exception Exception(string message)
            => new AssertFailedException(message);

        public Exception Exception(string message, Exception inner)
            => new AssertFailedException(message, inner);
    }
}