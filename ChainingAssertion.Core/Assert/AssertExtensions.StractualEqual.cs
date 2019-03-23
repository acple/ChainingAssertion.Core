using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static ChainingAssertion.AssertionService;

namespace ChainingAssertion
{
    public static partial class AssertExtensions
    {
        /// <summary>verifies that <paramref name="actual"/> is structurally equal to <paramref name="expected"/></summary>
        public static void IsStructuralEqual<T>(this T actual, T expected, string message = "")
            => StructuralEqual(expected, actual, typeof(T).Name, Message.Format(message));

        /// <summary>verifies that <paramref name="actual"/> is structurally equal to <paramref name="expected"/></summary>
        public static async Task IsStructuralEqual<T>(this Task<T> actual, T expected, string message = "")
            => (await actual.ConfigureAwait(false)).IsStructuralEqual(expected, message);

        /// <summary>verifies that <paramref name="actual"/> is not structurally equal to <paramref name="expected"/></summary>
        public static void IsNotStructuralEqual<T>(this T actual, T expected, string message = "")
        {
            try
            {
                StructuralEqual(expected, actual, string.Empty, string.Empty);
            }
            catch (Exception exception) when (exception.GetType() == Assertion.ExceptionType)
            {
                return;
            }
            throw Assertion.Exception("is structural equal" + Message.Format(message));
        }

        /// <summary>verifies that <paramref name="actual"/> is not structurally equal to <paramref name="expected"/></summary>
        public static async Task IsNotStructuralEqual<T>(this Task<T> actual, T expected, string message = "")
            => (await actual.ConfigureAwait(false)).IsNotStructuralEqual(expected, message);

        private static void StructuralEqual(object left, object right, string name, string message)
        {
            // basic checks
            if (ReferenceEquals(left, right))
                return;
            if (left == null || right == null)
                throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left ?? "null"} actual = {right ?? "null"}{message}");

            var type = left.GetType();

            // runtime type
            var rtype = right.GetType();
            if (type != rtype)
                throw Assertion.Exception($"is not structural equal, failed at {name}, expected = Type<{type}> actual = Type<{rtype}>{message}");

            // is string or primitive
            if (type == typeof(string) || type.GetTypeInfo().IsPrimitive)
            {
                if (!left.Equals(right))
                    throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left} actual = {right}{message}");
                return;
            }

            // is sequence
            if (left is IEnumerable le)
            {
                SequenceEqual(le, right as IEnumerable, name, message);
                return;
            }

            // is IStructuralEquatable
            if (left is IStructuralEquatable ls)
            {
                var index = 0;
                ls.Equals(right, new AssertEqualityComparer<object>((x, y) => { StructuralEqual(x, y, name + "{" + index++ + "}", message); return true; }));
                return;
            }

            // is IEquatable<T>
            var equatable = typeof(IEquatable<>)
                .MakeGenericType(type)
                .GetTypeInfo();

            if (equatable.IsAssignableFrom(type.GetTypeInfo()))
            {
                if (!(bool)equatable.GetDeclaredMethod("Equals").Invoke(left, new[] { right }))
                    throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left} actual = {right}{message}");
                return;
            }

            // is IComparable<T>
            var comparable = typeof(IComparable<>)
                .MakeGenericType(type)
                .GetTypeInfo();

            if (comparable.IsAssignableFrom(type.GetTypeInfo()))
            {
                if (0 != (int)comparable.GetDeclaredMethod("CompareTo").Invoke(left, new[] { right }))
                    throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left} actual = {right}{message}");
                return;
            }

            // is IComparable
            if (left is IComparable lc)
            {
                if (0 != lc.CompareTo(right))
                    throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left} actual = {right}{message}");
                return;
            }

            // is object
            // compare all fields
            foreach (var field in type.GetRuntimeFields())
            {
                var memberName = (field.Name.StartsWith("<")) // backingfield
                    ? field.Name.Substring(1, field.Name.IndexOf('>') - 1)
                    : field.Name;
                StructuralEqual(field.GetValue(left), field.GetValue(right), name + "." + memberName, message);
            }
        }

        private static void SequenceEqual(IEnumerable leftEnumerable, IEnumerable rightEnumerable, string name, string message)
        {
            var leftArray = leftEnumerable.Cast<object>().ToArray();
            var rightArray = rightEnumerable.Cast<object>().ToArray();

            if (leftArray.Length != rightArray.Length)
                throw Assertion.Exception($"is not structural equal, failed at {name}, sequence Length is different: expected = [{leftArray.Length}] actual = [{rightArray.Length}]{message}");

            var items = leftArray.Zip(rightArray, ValueTuple.Create)
                .Select((tuple, index) => (tuple, index));

            foreach (var ((left, right), i) in items)
                StructuralEqual(left, right, name + "[" + i + "]", message);
        }
    }
}
