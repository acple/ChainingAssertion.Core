namespace ChainingAssertion
{
    internal partial class AssertionService
    {
        public static IAssertionService Assertion { get; }

        static AssertionService()
        {
            Assertion = new AssertionService();
        }
    }
}
