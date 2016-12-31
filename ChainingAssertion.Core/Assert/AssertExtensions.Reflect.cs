namespace ChainingAssertion
{
    public static partial class AssertExtensions
    {
        /// <summary>get object member value with specified name</summary>
        public static object Reflect<T>(this T target, string name)
            => ReflectAccessor.Reflect(target, name);

        /// <summary>set object member value with specified name</summary>
        public static void ReflectSet<T>(this T target, string name, object value)
            => new ReflectAccessor(target, typeof(T))[name] = value;
    }
}
