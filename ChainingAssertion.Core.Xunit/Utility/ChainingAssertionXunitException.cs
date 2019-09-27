using System;
using Xunit.Sdk;

namespace ChainingAssertion
{
    /// <summary><see cref="XunitException"/> thrown by ChainingAssertion</summary>
    public sealed class ChainingAssertionXunitException : XunitException
    {
        internal ChainingAssertionXunitException()
        { }

        internal ChainingAssertionXunitException(string userMessage) : base(userMessage)
        { }

        internal ChainingAssertionXunitException(string userMessage, Exception innerException) : base(userMessage, innerException)
        { }
    }
}
