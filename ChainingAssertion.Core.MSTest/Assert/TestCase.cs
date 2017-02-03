using System;
using System.Collections.Generic;
using static ChainingAssertion.AssertionService;

namespace ChainingAssertion
{
    /// <summary>testcase extensions</summary>
    public static partial class TestCase
    {
        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T>(this IEnumerable<T> source, Action<T> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(parameter);
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2>(this IEnumerable<(T1, T2)> source, Action<T1, T2> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
                        parameter.Item1,
                        parameter.Item2
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3>(this IEnumerable<(T1, T2, T3)> source, Action<T1, T2, T3> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4>(this IEnumerable<(T1, T2, T3, T4)> source, Action<T1, T2, T3, T4> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5>(this IEnumerable<(T1, T2, T3, T4, T5)> source, Action<T1, T2, T3, T4, T5> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6>(this IEnumerable<(T1, T2, T3, T4, T5, T6)> source, Action<T1, T2, T3, T4, T5, T6> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> source, Action<T1, T2, T3, T4, T5, T6, T7> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
                        parameter.Item1,
                        parameter.Item2,
                        parameter.Item3,
                        parameter.Item4,
                        parameter.Item5,
                        parameter.Item6,
                        parameter.Item7,
                        parameter.Item8,
                        parameter.Item9
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
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
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
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
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
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
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
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
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
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
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
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
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }

        /// <summary>Run Parameterized Test.</summary>
        public static void RunTestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> source, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> assertion)
        {
            foreach (var parameter in source)
            {
                try
                {
                    assertion(
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
                        );
                }
                catch (Exception exception)
                {
                    throw Assertion.Exception($"failed in the case {parameter}: {exception}", exception);
                }
            }
        }
    }
}
