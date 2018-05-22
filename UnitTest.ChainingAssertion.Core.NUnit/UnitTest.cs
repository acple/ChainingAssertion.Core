using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using NUnit.Framework;

namespace UnitTest.ChainingAssertion.Core.NUnit
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void IsTest()
        {
            // "Is" extend on all object and has three overloads.

            // This same as Assert.AreEqual(25, Math.Pow(5, 2))
            Math.Pow(5, 2).Is(25);

            // lambda predicate assertion.
            // This same as Assert.IsTrue("foobar".StartsWith("foo") && "foobar".EndWith("bar"))
            "foobar".Is(s => s.StartsWith("foo") && s.EndsWith("bar"));

            // has collection assert
            // This same as CollectionAssert.AreEqual(Enumerable.Range(1,5), new[]{1, 2, 3, 4, 5})
            Enumerable.Range(1, 5).Is(1, 2, 3, 4, 5);
        }

        [Test]
        public void CollectionTest()
        {
            // if you want to use CollectionAssert Methods then use Linq to Objects and Is
            new[] { 1, 3, 7, 8 }.Contains(8).Is(true);
            new[] { 1, 3, 7, 8 }.Count(i => i % 2 != 0).Is(3);
            new[] { 1, 3, 7, 8 }.Any().Is(true);
            new[] { 1, 3, 7, 8 }.All(i => i < 5).Is(false);

            // IsOrdered
            var array = new[] { 1, 5, 10, 100 };
            array.Is(array.OrderBy(x => x));
        }

        [Test]
        public void OthersTest()
        {
            // Null Assertions
            Object obj = null;
            obj.IsNull();             // Assert.IsNull(obj)
            new Object().IsNotNull(); // Assert.IsNotNull(obj)

            // Not Assertion
            "foobar".IsNot("fooooooo"); // Assert.AreNotEqual
            new[] { "a", "z", "x" }.IsNot("a", "x", "z"); /// CollectionAssert.AreNotEqual

            // ReferenceEqual Assertion
            var tuple = Tuple.Create("foo");
            tuple.IsSameReferenceAs(tuple); // Assert.AreSame
            tuple.IsNotSameReferenceAs(Tuple.Create("foo")); // Assert.AreNotSame

            // Type Assertion
            "foobar".IsInstanceOf<string>(); // Assert.IsInstanceOf
            (999).IsNotInstanceOf<double>(); // Assert.IsNotInstanceOf
        }

        [Test]
        public void AdvancedCollectionTest()
        {
            var lower = new[] { "a", "b", "c" };
            var upper = new[] { "A", "B", "C" };

            // Comparer CollectionAssert, use IEqualityComparer<T> or Func<T,T,bool> delegate
            lower.Is(upper, StringComparer.OrdinalIgnoreCase);
            lower.Is(upper, (x, y) => x.ToUpper() == y.ToUpper());

            // or you can use Linq to Objects - SequenceEqual
            lower.SequenceEqual(upper, StringComparer.OrdinalIgnoreCase).Is(true);
        }

        [Test]
        public void ExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => "foo".StartsWith(null));
            Assert.Catch<Exception>(() => "foo".StartsWith(null));

            Assert.DoesNotThrow(() =>
            {
                // code
            });
        }

        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(10, 20, 30)]
        [TestCase(100, 200, 300)]
        public void TestCaseTest(int x, int y, int z)
        {
            (x + y).Is(z);
            (x + y + z).Is(i => i < 1000);
        }

        [Test]
        [TestCaseSource("toaruSource")]
        public void TestTestCaseSource(int x, int y, string z)
        {
            string.Concat(x, y).Is(z);
        }

        public static object[] toaruSource = new[]
        {
            new object[] {1, 1, "11"},
            new object[] {5, 3, "53"},
            new object[] {9, 4, "94"}
        };

        private class Person
        {
            public int Age { get; set; }
            public string FamilyName { get; set; }
            public string GivenName { get; set; }
        }

        [Test]
        public void IsNullMethodMessage()
        {

            object o = new object();
            o.IsNotNull();
            Assert.Throws<AssertionException>(
                () => o.IsNull("msg_msg"))
            .Message.Contains("msg_msg").Is(true);

            o = null;
            o.IsNull();
            Assert.Throws<AssertionException>(
                () => o.IsNotNull("msg_msg"))
            .Message.Contains("msg_msg").Is(true);
        }

        public class StructuralEqualTestClass
        {
            public int IntPro { get; set; }
            public string StrProp { get; set; }
            public int IntField;
            public string StrField;
            public int SetOnlyProp { private get; set; }
            public int[] IntArray { get; set; }
            public Stru StruStru;

            static Random rand = new Random();

            public StructuralEqualTestClass()
            {
                this.SetOnlyProp = 123456; // rand.Next();
            }
        }

        public class DummyStructural : IEquatable<DummyStructural>
        {
            public string MyProperty { get; set; }

            public bool Equals(DummyStructural other)
            {
                return true;
            }
        }

        public class Stru
        {
            public int MyProperty { get; set; }
            public string[] StrArray { get; set; }
            public MMM MP2 { get; set; }
        }

        public class MMM
        {
            public int MyProperty { get; set; }
        }

        public class EmptyClass
        {

        }

        [Test]
        public void StructuralEqualSuccess()
        {
            // primitive
            "hoge".IsStructuralEqual("hoge");
            (100).IsStructuralEqual(100);
            new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2, 3 });

            // complex
            new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }.IsStructuralEqual(new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } });
            new DummyStructural() { MyProperty = "aiueo" }.IsStructuralEqual(new DummyStructural() { MyProperty = "kakikukeko" });
            new EmptyClass().IsStructuralEqual(new EmptyClass());

            var s1 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 10000 }
                }
            };

            var s2 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 10000 }
                }
            };

            s1.IsStructuralEqual(s1);
            s1.IsStructuralEqual(s2);
        }

        [Test]
        public void StructuralEqualFailed()
        {
            // type
            object n = null;
            Assert.Throws<AssertionException>(() => n.IsStructuralEqual("a"));
            Assert.Throws<AssertionException>(() => "a".IsStructuralEqual(n));

            // primitive
            Assert.Throws<AssertionException>(() => "hoge".IsStructuralEqual("hage"))
                .Message.Is(m => m.Contains("expected = hage actual = hoge"));
            Assert.Throws<AssertionException>(() => (100).IsStructuralEqual(101))
                .Message.Is(m => m.Contains("expected = 101 actual = 100"));

            Assert.Throws<AssertionException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2 }))
                .Message.Is(m => m.Contains("sequence Length is different: expected = [2] actual = [3]"));

            Assert.Throws<AssertionException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2, 4 }))
                .Message.Is(m => m.Contains("expected = 4 actual = 3"));

            Assert.Throws<AssertionException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2, 3, 4 }))
                .Message.Is(m => m.Contains("sequence Length is different: expected = [4] actual = [3]"));

            Assert.Throws<AssertionException>(() => new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }.IsStructuralEqual(new { Hoge = "aiueo", Huga = 100, Tako = new { k = 12 } }))
                .Message.Is(m => m.Contains("expected = 12 actual = 10"));

            var s1 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 10000 }
                }
            };

            var s2 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5, 6 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 10000 }
                }
            };

            var s3 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 13000 }
                }
            };

            Assert.Throws<AssertionException>(() => s1.IsStructuralEqual(s2))
                .Message.Is(m => m.Contains("at StructuralEqualTestClass.IntArray, sequence Length is different: expected = [6] actual = [5]"));

            Assert.Throws<AssertionException>(() => s1.IsStructuralEqual(s3))
                .Message.Is(m => m.Contains("StructuralEqualTestClass.StruStru.MP2.MyProperty"));
        }


        [Test]
        public void NotStructuralEqualFailed()
        {
            // primitive
            Assert.Throws<AssertionException>(() => "hoge".IsNotStructuralEqual("hoge"));
            Assert.Throws<AssertionException>(() => (100).IsNotStructuralEqual(100));
            Assert.Throws<AssertionException>(() => new[] { 1, 2, 3 }.IsNotStructuralEqual(new[] { 1, 2, 3 }));

            // complex
            Assert.Throws<AssertionException>(() => new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }.IsNotStructuralEqual(new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }));
            Assert.Throws<AssertionException>(() => new DummyStructural() { MyProperty = "aiueo" }.IsNotStructuralEqual(new DummyStructural() { MyProperty = "kakikukeko" }));
            Assert.Throws<AssertionException>(() => new EmptyClass().IsNotStructuralEqual(new EmptyClass()));

            var s1 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 10000 }
                }
            };

            var s2 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 10000 }
                }
            };

            Assert.Throws<AssertionException>(() => s1.IsNotStructuralEqual(s1));
            Assert.Throws<AssertionException>(() => s1.IsNotStructuralEqual(s2));
        }

        [Test]
        public void NotStructuralEqualSuccess()
        {
            // type
            object n = null;
            n.IsNotStructuralEqual("a");
            "a".IsNotStructuralEqual(n);

            // primitive
            "hoge".IsNotStructuralEqual("hage");
            (100).IsNotStructuralEqual(101);

            new[] { 1, 2, 3 }.IsNotStructuralEqual(new[] { 1, 2 });

            new[] { 1, 2, 3 }.IsNotStructuralEqual(new[] { 1, 2, 4 });

            new[] { 1, 2, 3 }.IsNotStructuralEqual(new[] { 1, 2, 3, 4 });

            new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }.IsNotStructuralEqual(new { Hoge = "aiueo", Huga = 100, Tako = new { k = 12 } });

            var s1 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 10000 }
                }
            };

            var s2 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5, 6 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 10000 }
                }
            };

            var s3 = new StructuralEqualTestClass
            {
                IntPro = 1,
                IntField = 10,
                StrField = "hoge",
                StrProp = "huga",
                IntArray = new[] { 1, 2, 3, 4, 5 },
                StruStru = new Stru()
                {
                    MyProperty = 1000,
                    StrArray = new[] { "hoge", "huga", "tako" },
                    MP2 = new MMM() { MyProperty = 13000 }
                }
            };

            s1.IsNotStructuralEqual(s2);

            s1.IsNotStructuralEqual(s3);
        }
    }
}
