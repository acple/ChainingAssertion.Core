using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;

namespace UnitTest.ChainingAssertion.Core.Xunit
{
    public class UnitTest
    {
        [Fact]
        public void IsTest()
        {
            // "Is" extend on all object and has three overloads.

            // This same as Assert.Equal(25, Math.Pow(5, 2))
            Math.Pow(5, 2).Is(25);

            // lambda predicate assertion.
            // This same as Assert.True("foobar".StartsWith("foo") && "foobar".EndWith("bar"))
            "foobar".Is(s => s.StartsWith("foo") && s.EndsWith("bar"));

            // has collection assert
            // This same as Assert.Equal(Enumerable.Range(1,5).ToArray(), new[]{1, 2, 3, 4, 5}.ToArray())
            // it is sequence value compare
            Enumerable.Range(1, 5).Is(1, 2, 3, 4, 5);
        }

        [Fact]
        public void CollectionTest()
        {
            // if you want to use CollectionAssert Methods then use Linq to Objects and Is
            new[] { 1, 3, 7, 8 }.Contains(8).IsTrue();
            new[] { 1, 3, 7, 8 }.Count(i => i % 2 != 0).Is(3);
            new[] { 1, 3, 7, 8 }.IsNotEmpty();
            new[] { 1, 3, 7, 8 }.All(i => i < 5).IsFalse();

            // IsOrdered
            var array = new[] { 1, 5, 10, 100 };
            array.Is(array.OrderBy(x => x).ToArray());
        }

        [Fact]
        public void OthersTest()
        {
            // Null Assertions
            var obj = null as object;
            obj.IsNull();             // Assert.Null(obj)
            new object().IsNotNull(); // Assert.NotNull(obj)

            // Not Assertion
            "foobar".IsNot("fooooooo"); // Assert.NotEqual
            new[] { "a", "z", "x" }.IsNot("a", "x", "z"); /// Assert.NotEqual

            // ReferenceEqual Assertion
            var tuple = Tuple.Create("foo");
            tuple.IsSameReferenceAs(tuple); // Assert.Same
            tuple.IsNotSameReferenceAs(Tuple.Create("foo")); // Assert.NotSame

            // Type Assertion
            "foobar".IsInstanceOf<string>(); // Assert.IsType
            (999).IsNotInstanceOf<double>(); // Assert.IsNotType
        }

        [Fact]
        public void AdvancedCollectionTest()
        {
            var lower = new[] { "a", "b", "c" };
            var upper = new[] { "A", "B", "C" };

            // Comparer CollectionAssert, use IEqualityComparer<T> or Func<T,T,bool> delegate
            lower.Is(upper, StringComparer.OrdinalIgnoreCase);
            lower.Is(upper, (x, y) => x.ToUpper() == y.ToUpper());

            // or you can use Linq to Objects - SequenceEqual
            lower.SequenceEqual(upper, StringComparer.OrdinalIgnoreCase).IsTrue();
        }

        [Fact]
        public void ExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => "foo".StartsWith(null));

            //Assert.DoesNotThrow(() =>
            //{
            //    // code
            //});
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(10, 20, 30)]
        [InlineData(100, 200, 300)]
        public void TestCaseTest(int x, int y, int z)
        {

            (x + y).Is(z);
            (x + y + z).Is(i => i < 1000);
        }

        [Theory]
        [MemberData("ToaruSource")]
        public void TestTestCaseSource(int x, int y, string z)
        {
            string.Concat(x, y).Is(z);
        }

        public static object[] ToaruSource
        {
            get
            {
                return new[]
                {
                    new object[] {1, 1, "11"},
                    new object[] {5, 3, "53"},
                    new object[] {9, 4, "94"}
                };
            }
        }

        private class Person
        {
            public int Age { get; set; }
            public string FamilyName { get; set; }
            public string GivenName { get; set; }
        }

        [Fact]
        public void DumpTest()
        {
            var count = new List<int>() { 1, 2, 3 };
            var person = new Person { Age = 50, FamilyName = "Yamamoto", GivenName = "Tasuke" };
            try
            {
                person.Is(p => p.Age < count.Count && p.FamilyName == "Yamada" && p.GivenName == "Tarou");
            }
            catch (Exception ex)
            {
                ex.Message.Is(m => m.Contains("Age = 50") && m.Contains("FamilyName = Yamamoto") && m.Contains("GivenName = Tasuke"));
                return;
            }
            Assert.True(false);
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
                SetOnlyProp = 123456; // rand.Next();
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

        [Fact]
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

        [Fact]
        public void StructuralEqualFailed()
        {
            //// type
            object n = null;
            Assert.Throws<ChainingAssertionException>(() => n.IsStructuralEqual("a"));
            Assert.Throws<ChainingAssertionException>(() => "a".IsStructuralEqual(n));
            //int i = 10;
            //long l = 10;
            //Assert.Throws<ChainingAssertionException>(() => i.IsStructuralEqual(l));

            // primitive
            Assert.Throws<ChainingAssertionException>(() => "hoge".IsStructuralEqual("hage"))
                .Message.Is(m => m.Contains("expected = hage actual = hoge"));
            Assert.Throws<ChainingAssertionException>(() => (100).IsStructuralEqual(101))
                .Message.Is(m => m.Contains("expected = 101 actual = 100"));

            Assert.Throws<ChainingAssertionException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2 }))
                .Message.Is(m => m.Contains("sequence Length is different: expected = [2] actual = [3]"));

            Assert.Throws<ChainingAssertionException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2, 4 }))
                .Message.Is(m => m.Contains("expected = 4 actual = 3"));

            Assert.Throws<ChainingAssertionException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2, 3, 4 }))
                .Message.Is(m => m.Contains("sequence Length is different: expected = [4] actual = [3]"));

            Assert.Throws<ChainingAssertionException>(() => new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }.IsStructuralEqual(new { Hoge = "aiueo", Huga = 100, Tako = new { k = 12 } }))
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

            Assert.Throws<ChainingAssertionException>(() => s1.IsStructuralEqual(s2))
                .Message.Is(m => m.Contains("at StructuralEqualTestClass.IntArray, sequence Length is different: expected = [6] actual = [5]"));

            Assert.Throws<ChainingAssertionException>(() => s1.IsStructuralEqual(s3))
                .Message.Is(m => m.Contains("StructuralEqualTestClass.StruStru.MP2.MyProperty"));
        }


        [Fact]
        public void NotStructuralEqualFailed()
        {
            // primitive
            Assert.Throws<ChainingAssertionException>(() => "hoge".IsNotStructuralEqual("hoge"));
            Assert.Throws<ChainingAssertionException>(() => (100).IsNotStructuralEqual(100));
            Assert.Throws<ChainingAssertionException>(() => new[] { 1, 2, 3 }.IsNotStructuralEqual(new[] { 1, 2, 3 }));

            // complex
            Assert.Throws<ChainingAssertionException>(() => new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }.IsNotStructuralEqual(new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }));
            Assert.Throws<ChainingAssertionException>(() => new DummyStructural() { MyProperty = "aiueo" }.IsNotStructuralEqual(new DummyStructural() { MyProperty = "kakikukeko" }));
            Assert.Throws<ChainingAssertionException>(() => new EmptyClass().IsNotStructuralEqual(new EmptyClass()));

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

            Assert.Throws<ChainingAssertionException>(() => s1.IsNotStructuralEqual(s1));
            Assert.Throws<ChainingAssertionException>(() => s1.IsNotStructuralEqual(s2));
        }

        [Fact]
        public void NotStructuralEqualSuccess()
        {
            //// type
            object n = null;
            n.IsNotStructuralEqual("a");
            "a".IsNotStructuralEqual(n);
            //int i = 10;
            //long l = 10;
            //i.IsNotStructuralEqual(l);

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
