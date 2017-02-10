using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChainingAssertion
{
    internal partial class AssertionService : IAssertionService
    {
        public void Equal<T>(T expected, T actual, string message)
            => Assert.Equal(expected, actual);

        public void NotEqual<T>(T expected, T actual, string message)
            => Assert.NotEqual(expected, actual);

        public void Equal<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer, string message)
            => Assert.True(actual.SequenceEqual(expected, comparer), message);

        public void NotEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer, string message)
            => Assert.False(actual.SequenceEqual(expected, comparer), message);

        public void Null<T>(T value, string message)
            => Assert.Null(value);

        public void NotNull<T>(T value, string message)
            => Assert.NotNull(value);

        public void True(bool condition, string message)
            => Assert.True(condition, message);

        public void False(bool condition, string message)
            => Assert.False(condition, message);

        public void Fail(string message)
            => Assert.True(false, message);

        public void ReferenceEqual<T>(T expected, T actual, string message)
            => Assert.Same(expected, actual);

        public void NotReferenceEqual<T>(T expected, T actual, string message)
            => Assert.NotSame(expected, actual);

        public void InstanceOf<T>(object value, string message)
            => Assert.IsAssignableFrom<T>(value);

        public void NotInstanceOf<T>(object value, string message)
            => Assert.IsNotType<T>(value);

        public Exception Exception()
            => new ChainingAssertionException();

        public Exception Exception(string message)
            => new ChainingAssertionException(message);

        public Exception Exception(string message, Exception inner)
            => new ChainingAssertionException(message, inner);
    }
}
