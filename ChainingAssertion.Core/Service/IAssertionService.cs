using System;
using System.Collections.Generic;

namespace ChainingAssertion
{
    internal interface IAssertionService
    {
        Type ExceptionType { get; }

        Exception Exception(string message);

        Exception Exception(string message, Exception inner);

        void Equal<T>(T expected, T actual, string message);

        void NotEqual<T>(T expected, T actual, string message);

        void Equal<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer, string message);

        void NotEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer, string message);

        void Null<T>(T value, string message);

        void NotNull<T>(T value, string message);

        void True(bool condition, string message);

        void False(bool condition, string message);

        void Fail(string message);

        void ReferenceEqual<T>(T expected, T actual, string message);

        void NotReferenceEqual<T>(T expected, T actual, string message);

        void InstanceOf<T>(object value, string message);

        void NotInstanceOf<T>(object value, string message);
    }
}
