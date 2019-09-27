namespace ChainingAssertion
{
    public static partial class AssertExtensions
    {
        /// <summary>get object member value with specified name</summary>
        public static object? Reflect<T>(this T target, string name)
            where T : notnull
            => ReflectAccessor.Reflect(target, name);

        /// <summary>set object member value with specified name</summary>
        public static void ReflectSet<T>(this T target, string name, object value)
            where T : notnull
            => new ReflectAccessor(target, typeof(T))[name] = value;
    }
}
