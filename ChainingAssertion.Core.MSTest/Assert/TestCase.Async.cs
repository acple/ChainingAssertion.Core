using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ChainingAssertion.AssertionService;

namespace ChainingAssertion
{
    public static partial class TestCase
    {
        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T>(this IEnumerable<T> source, Func<T, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(parameter).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2>(this IEnumerable<(T1, T2)> source, Func<T1, T2, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3>(this IEnumerable<(T1, T2, T3)> source, Func<T1, T2, T3, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4>(this IEnumerable<(T1, T2, T3, T4)> source, Func<T1, T2, T3, T4, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5>(this IEnumerable<(T1, T2, T3, T4, T5)> source, Func<T1, T2, T3, T4, T5, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6>(this IEnumerable<(T1, T2, T3, T4, T5, T6)> source, Func<T1, T2, T3, T4, T5, T6, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> source, Func<T1, T2, T3, T4, T5, T6, T7, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9,
                        parameter.Item10
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9,
                        parameter.Item10,
                        parameter.Item11
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9,
                        parameter.Item10,
                        parameter.Item11,
                        parameter.Item12
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9,
                        parameter.Item10,
                        parameter.Item11,
                        parameter.Item12,
                        parameter.Item13
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9,
                        parameter.Item10,
                        parameter.Item11,
                        parameter.Item12,
                        parameter.Item13,
                        parameter.Item14
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9,
                        parameter.Item10,
                        parameter.Item11,
                        parameter.Item12,
                        parameter.Item13,
                        parameter.Item14,
                        parameter.Item15
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static async Task RunTestCaseAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> source, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    await assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9,
                        parameter.Item10,
                        parameter.Item11,
                        parameter.Item12,
                        parameter.Item13,
                        parameter.Item14,
                        parameter.Item15,
                        parameter.Item16
                        ).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }
    }
}
