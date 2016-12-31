using System;
using Xunit.Sdk;

namespace ChainingAssertion
{
    /// <summary><see cref="XunitException"/> thrown by ChainingAssertion</summary>
    public sealed class ChainingAssertionException : XunitException
    {
        internal ChainingAssertionException()
        { }

        internal ChainingAssertionException(string userMessage) : base(userMessage)
        { }

        internal ChainingAssertionException(string userMessage, Exception innerException) : base(userMessage, innerException)
        { }
    }
}
