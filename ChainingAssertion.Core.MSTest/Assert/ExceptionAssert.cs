using System;
using static ChainingAssertion.AssertionService;

namespace ChainingAssertion
{
    /// <summary>exception assertion methods</summary>
    public static partial class ExceptionAssert
    {
        /// <summary>verifies that the exception type of <typeparamref name="T"/> or derived is thrown</summary>
        public static T Catch<T>(Action testCode, string message = "") where T : Exception
        {
            try
            {
                testCode();
            }
            catch (T exception)
            {
                return exception;
            }
            catch (Exception exception)
            {
                throw Assertion.Exception($"Failed Throws<{typeof(T).Name}>. Catched:{exception.GetType().Name}{Message.Format(message)}", exception);
            }
            throw Assertion.Exception($"Failed Throws<{typeof(T).Name}>. No exception was thrown{Message.Format(message)}");
        }

        /// <summary>verifies that the exception type of <typeparamref name="T"/> is thrown (not allow derived type)</summary>
        public static T Throws<T>(Action testCode, string message = "") where T : Exception
        {
            try
            {
                testCode();
            }
            catch (T exception) when (exception.GetType() == typeof(T))
            {
                return exception;
            }
            catch (Exception exception)
            {
                throw Assertion.Exception($"Failed Throws<{typeof(T).Name}>. Catched:{exception.GetType().Name}{Message.Format(message)}", exception);
            }
            throw Assertion.Exception($"Failed Throws<{typeof(T).Name}>. No exception was thrown{Message.Format(message)}");
        }
    }
}
