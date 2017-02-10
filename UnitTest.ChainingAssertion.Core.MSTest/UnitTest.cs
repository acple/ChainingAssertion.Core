using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainingAssertion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.ChainingAssertion.Core.MSTest
{
    [TestClass]
    public class UnitTest
    {
        // samples

        [TestMethod]
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

        [TestMethod]
        public void CollectionTest()
        {
            // if you want to use CollectionAssert Methods then use Linq to Objects and Is

            var array = new[] { 1, 3, 7, 8 };
            array.Count().Is(4);
            array.Contains(8).IsTrue(); // IsTrue() == Is(true)
            array.All(i => i < 5).IsFalse(); // IsFalse() == Is(false)
            array.Any().Is(true);
            new int[] { }.IsEmpty();   // IsEmpty
            array.OrderBy(x => x).Is(array); // IsOrdered
        }

        [TestMethod]
        public void OthersTest()
        {
            // Null Assertions
            var obj = null as object;
            obj.IsNull();             // Assert.IsNull(obj)
            new object().IsNotNull(); // Assert.IsNotNull(obj)

            // Not Assertion
            "foobar".IsNot("fooooooo"); // Assert.AreNotEqual
            new[] { "a", "z", "x" }.IsNot("a", "x", "z"); /// CollectionAssert.AreNotEqual

            // ReferenceEqual Assertion
            var tuple = Tuple.Create("foo");
            tuple.IsSameReferenceAs(tuple); // Assert.AreSame
            tuple.IsNotSameReferenceAs(Tuple.Create("foo")); // Assert.AreNotSame

            // Type Assertion
            var foobar = default(object);
            foobar = "foobar";
            foobar.IsInstanceOf<string>() // Assert.IsInstanceOfType
                .ToUpper().Is("FOOBAR");            // ...returns the instance as TExpected.
            (999).IsNotInstanceOf<double>(); // Assert.IsNotInstanceOfType
        }

        [TestMethod]
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

        class MyClass
        {
            public int IntProp { get; set; }
            public string StrField;
        }

        [TestMethod]
        public void StructuralEqualTest()
        {
            var mc1 = new MyClass() { IntProp = 10, StrField = "foo" };
            var mc2 = new MyClass() { IntProp = 10, StrField = "foo" };

            mc1.IsStructuralEqual(mc2); // deep recursive value equality compare

            mc1.IntProp = 20;
            mc1.IsNotStructuralEqual(mc2);
        }

        [TestMethod]
        public void ExceptionTest()
        {
            // Exception Test(alternative of ExpectedExceptionAttribute)
            // Throws does not allow derived type
            // Catch allows derived type
            ExceptionAssert.Throws<ArgumentNullException>(() => "foo".StartsWith(null));
            ExceptionAssert.Catch<Exception>(() => "foo".StartsWith(null));

            // return value is occured exception
            var ex = ExceptionAssert.Throws<InvalidOperationException>(() =>
            {
                throw new InvalidOperationException("foobar operation");
            });
            ex.Message.Is(m => m.Contains("foobar")); // additional exception assertion
        }

        private class Person
        {
            public int Age { get; set; }
            public string FamilyName { get; set; }
            public string GivenName { get; set; }
        }

        [TestMethod]
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
                ex.Message.Is(m => m.Contains("p.Age = 50") && m.Contains("p.FamilyName = Yamamoto") && m.Contains("p.GivenName = Tasuke"));
                return;
            }
            Assert.Fail();
        }

        // testcase

        [TestMethod]
        public void RunTestCaseTest()
        {
            var source = new[]
            {
                (1, 2, 3),
                (10, 20, 30),
                (100, 200, 300)
            };

            source.RunTestCase((x, y, z) =>
            {
                (x + y).Is(z);
                (x + y + z).Is(i => i < 1000);
            });

            var toaruSource = new[]
            {
                (1, 1, "11"),
                (5, 3, "53"),
                (9, 4, "94")
            };

            toaruSource.RunTestCase((x, y, result) =>
            {
                string.Concat(x, y).Is(result);
            });
        }

        [TestMethod]
        public async Task RunTestCaseAsyncTest()
        {
            var source = new[]
            {
                (1, 2, 3),
                (10, 20, 30),
                (100, 200, 300)
            };

            await source.RunTestCaseAsync(async (x, y, z) =>
            {
                await Task.Run(() => (x + y).Is(z));
                (x + y + z).Is(i => i < 1000);
            });

            var toaruSource = new[]
            {
                (1, 1, "11"),
                (5, 3, "53"),
                (9, 4, "94")
            };

            await toaruSource.RunTestCaseAsync(async (x, y, result) =>
            {
                await Task.Run(() => string.Concat(x, y).Is(result));
            });
        }

        // exceptions

        [TestMethod]
        public void ThrowsTest()
        {
            try
            {
                ExceptionAssert.Throws<Exception>(() => "foo".StartsWith(null));
            }
            catch (AssertFailedException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void CatchTest()
        {
            try
            {
                ExceptionAssert.Catch<Exception>(() => "foo".StartsWith(null));
            }
            catch (AssertFailedException)
            {
                Assert.Fail();
            }
            return;
        }

        [TestMethod]
        public void ThrowsTest2()
        {
            try
            {
                ExceptionAssert.Throws<Exception>(() => { });
            }
            catch (AssertFailedException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void CatchTest2()
        {
            try
            {
                ExceptionAssert.Catch<Exception>(() => { });
            }
            catch (AssertFailedException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExceptionTest2()
        {
            var ex = ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                throw new ArgumentNullException("nullnull");
            });
            ex.ParamName.Is("nullnull");
        }

        [TestMethod]
        public void IsNullMessageTest()
        {
            var o = new object();
            o.IsNotNull();
            ExceptionAssert.Throws<AssertFailedException>(
                () => o.IsNull("msg_msg"))
            .Message.Is(m => m.Contains("msg_msg"));

            o = null;
            o.IsNull();
            ExceptionAssert.Throws<AssertFailedException>(
                () => o.IsNotNull("msg_msg"))
            .Message.Is(m => m.Contains("msg_msg"));
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

        [TestMethod]
        public void StructuralEqualSuccessTest()
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
            (123, "abc", s1).IsStructuralEqual((123, "abc", s2));
            s1.IsStructuralEqual(s1);
            s1.IsStructuralEqual(s2);
        }

        [TestMethod]
        public void StructuralEqualFailedTest()
        {
            // type
            //object n = null;
            //AssertEx.Throws<AssertFailedException>(() => n.IsStructuralEqual("a"));
            //AssertEx.Throws<AssertFailedException>(() => "a".IsStructuralEqual(n));
            //int i = 10;
            //long l = 10;
            //AssertEx.Throws<AssertFailedException>(() => i.IsStructuralEqual(l));

            // primitive
            ExceptionAssert.Throws<AssertFailedException>(() => "hoge".IsStructuralEqual("hage"))
                .Message.Is(m => m.Contains("expected = hage actual = hoge"));
            ExceptionAssert.Throws<AssertFailedException>(() => (100).IsStructuralEqual(101))
                .Message.Is(m => m.Contains("expected = 101 actual = 100"));

            ExceptionAssert.Throws<AssertFailedException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2 }))
                .Message.Is(m => m.Contains("sequence Length is different: expected = [2] actual = [3]"));

            ExceptionAssert.Throws<AssertFailedException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2, 4 }))
                .Message.Is(m => m.Contains("expected = 4 actual = 3"));

            ExceptionAssert.Throws<AssertFailedException>(() => new[] { 1, 2, 3 }.IsStructuralEqual(new[] { 1, 2, 3, 4 }))
                .Message.Is(m => m.Contains("sequence Length is different: expected = [4] actual = [3]"));

            ExceptionAssert.Throws<AssertFailedException>(() => new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }.IsStructuralEqual(new { Hoge = "aiueo", Huga = 100, Tako = new { k = 12 } }))
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

            ExceptionAssert.Throws<AssertFailedException>(() => s1.IsStructuralEqual(s2))
                .Message.Is(m => m.Contains("at StructuralEqualTestClass.IntArray, sequence Length is different: expected = [6] actual = [5]"));

            ExceptionAssert.Throws<AssertFailedException>(() => s1.IsStructuralEqual(s3))
                .Message.Is(m => m.Contains("StructuralEqualTestClass.StruStru.MP2.MyProperty"));
        }


        [TestMethod]
        public void NotStructuralEqualFailedTest()
        {
            // primitive
            ExceptionAssert.Throws<AssertFailedException>(() => "hoge".IsNotStructuralEqual("hoge"));
            ExceptionAssert.Throws<AssertFailedException>(() => (100).IsNotStructuralEqual(100));
            ExceptionAssert.Throws<AssertFailedException>(() => new[] { 1, 2, 3 }.IsNotStructuralEqual(new[] { 1, 2, 3 }));

            // complex
            ExceptionAssert.Throws<AssertFailedException>(() => new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }.IsNotStructuralEqual(new { Hoge = "aiueo", Huga = 100, Tako = new { k = 10 } }));
            ExceptionAssert.Throws<AssertFailedException>(() => new DummyStructural() { MyProperty = "aiueo" }.IsNotStructuralEqual(new DummyStructural() { MyProperty = "kakikukeko" }));
            ExceptionAssert.Throws<AssertFailedException>(() => new EmptyClass().IsNotStructuralEqual(new EmptyClass()));

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

            ExceptionAssert.Throws<AssertFailedException>(() => s1.IsNotStructuralEqual(s1));
            ExceptionAssert.Throws<AssertFailedException>(() => s1.IsNotStructuralEqual(s2));
        }

        [TestMethod]
        public void NotStructuralEqualSuccessTest()
        {
            // type
            //object n = null;
            //n.IsNotStructuralEqual("a");
            //"a".IsNotStructuralEqual(n);
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
