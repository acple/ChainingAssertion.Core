using System;
using System.Collections;
using System.Collections.Generic;

namespace ChainingAssertion
{
    /// <summary>comparer using lambda expression</summary>
    public class AssertEqualityComparer<T> : EqualityComparer<T>, IComparer<T>, IComparer
    {
        private readonly Func<T, T, bool> _equals;

        private readonly Func<T, int> _getHash;

        /// <summary>create comparer from instance of <see cref="IEqualityComparer{T}" /></summary>
        public AssertEqualityComparer(IEqualityComparer<T> comparer) : this(comparer.Equals, comparer.GetHashCode)
        { }

        /// <summary>create comparer from equals lambda expression</summary>
        public AssertEqualityComparer(Func<T, T, bool> equals) : this(equals, _ => 0)
        { }

        /// <summary>create comparer from equals and gethash lambda expression</summary>
        public AssertEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHash)
        {
            this._equals = equals;
            this._getHash = getHash;
        }

        /// <summary>implementation of <see cref="IEqualityComparer{T}.Equals(T, T)" /></summary>
        public override bool Equals(T x, T y)
            => this._equals(x, y);

        /// <summary>implementation of <see cref="IEqualityComparer{T}.GetHashCode(T)" /></summary>
        public override int GetHashCode(T obj)
            => this._getHash(obj);

        int IComparer<T>.Compare(T x, T y)
            => (this._equals(x, y)) ? 0 : -1;

        int IComparer.Compare(object? x, object? y)
            => (x is T a && y is T b && this._equals(a, b)) ? 0 : -1;
    }
}
