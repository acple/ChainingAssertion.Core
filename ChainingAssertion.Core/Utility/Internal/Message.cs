namespace ChainingAssertion
{
    internal static class Message
    {
        internal static string Format(string message)
            => (string.IsNullOrEmpty(message)) ? string.Empty : ", " + message;
    }
}
