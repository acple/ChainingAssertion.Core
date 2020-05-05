using System;
using System.Collections;
using System.Collections.Concurrent;
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
            => StructuralEqual.Run(expected, actual, typeof(T).Name, Message.Format(message));

        /// <summary>verifies that <paramref name="actual"/> is structurally equal to <paramref name="expected"/></summary>
        public static async Task IsStructuralEqual<T>(this Task<T> actual, T expected, string message = "")
            => (await actual.ConfigureAwait(false)).IsStructuralEqual(expected, message);

        /// <summary>verifies that <paramref name="actual"/> is not structurally equal to <paramref name="expected"/></summary>
        public static void IsNotStructuralEqual<T>(this T actual, T expected, string message = "")
        {
            try
            {
                StructuralEqual.Run(expected, actual, string.Empty, string.Empty);
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

        private static class StructuralEqual
        {
            private static readonly ConcurrentDictionary<Type, Action<object, object, string, string>> comparerCache =
                new ConcurrentDictionary<Type, Action<object, object, string, string>>();

            public static void Run(object? left, object? right, string name, string message)
            {
                if (ReferenceEquals(left, right))
                    return;

                if (left == null || right == null)
                    throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left ?? "null"} actual = {right ?? "null"}{message}");

                var type = left.GetType();
                var rtype = right.GetType();
                if (type != rtype)
                    throw Assertion.Exception($"is not structural equal, failed at {name}, expected = Type<{type}> actual = Type<{rtype}>{message}");

                var comparer = comparerCache.GetOrAdd(type, CreateComparer);
                comparer.Invoke(left, right, name, message);
            }

            private static Action<object, object, string, string> CreateComparer(Type type)
            {
                var typeInfo = type.GetTypeInfo();

                // is string or primitive
                if (type == typeof(string) || typeInfo.IsPrimitive)
                    return (left, right, name, message) =>
                    {
                        if (!left.Equals(right))
                            throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left} actual = {right}{message}");
                    };

                // is sequence
                if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(typeInfo))
                    return (left, right, name, message) => SequenceEqual((IEnumerable)left, (IEnumerable)right, name, message);

                // is IStructuralEquatable
                if (typeof(IStructuralEquatable).GetTypeInfo().IsAssignableFrom(typeInfo))
                    return (left, right, name, message) =>
                    {
                        var index = 0;
                        (left as IStructuralEquatable)!.Equals(right, new AssertEqualityComparer<object>((x, y) => { Run(x, y, name + "{" + index++ + "}", message); return true; }));
                    };

                // is IEquatable<T>
                var equatable = typeof(IEquatable<>)
                    .MakeGenericType(type)
                    .GetTypeInfo();

                if (equatable.IsAssignableFrom(typeInfo))
                {
                    var equals = equatable.GetDeclaredMethod("Equals")!;
                    return (left, right, name, message) =>
                    {
                        if (!(bool)equals.Invoke(left, new[] { right })!)
                            throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left} actual = {right}{message}");
                    };
                }

                // is IComparable<T>
                var comparable = typeof(IComparable<>)
                    .MakeGenericType(type)
                    .GetTypeInfo();

                if (comparable.IsAssignableFrom(typeInfo))
                {
                    var compareTo = comparable.GetDeclaredMethod("CompareTo")!;
                    return (left, right, name, message) =>
                    {
                        if (0 != (int)compareTo.Invoke(left, new[] { right })!)
                            throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left} actual = {right}{message}");
                    };
                }

                // is IComparable
                if (typeof(IComparable).GetTypeInfo().IsAssignableFrom(typeInfo))
                    return (left, right, name, message) =>
                    {
                        if (0 != (left as IComparable)!.CompareTo(right))
                            throw Assertion.Exception($"is not structural equal, failed at {name}, expected = {left} actual = {right}{message}");
                    };

                // is object
                // compare all fields
                var fields = type.GetRuntimeFields();
                return (left, right, name, message) =>
                {
                    foreach (var field in fields)
                    {
                        var memberName = field.Name.StartsWith("<") // backingfield
                            ? field.Name.Substring(1, field.Name.IndexOf('>') - 1)
                            : field.Name;
                        Run(field.GetValue(left), field.GetValue(right), name + "." + memberName, message);
                    }
                };
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
                    Run(left, right, name + "[" + i + "]", message);
            }
        }
    }
}
