using System;
using System.Threading.Tasks;
using static ChainingAssertion.AssertionService;

namespace ChainingAssertion
{
    public static partial class ExceptionAssert
    {
        /// <summary>verifies that the exception type of <typeparamref name="T"/> or derived is thrown</summary>
        public static async Task<T> CatchAsync<T>(Func<Task> testCode, string message = "") where T : Exception
        {
            try
            {
                await testCode().ConfigureAwait(false);
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
        public static async Task<T> ThrowsAsync<T>(Func<Task> testCode, string message = "") where T : Exception
        {
            try
            {
                await testCode().ConfigureAwait(false);
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
