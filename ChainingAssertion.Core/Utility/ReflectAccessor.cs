using System;
using System.Linq;
using System.Reflection;

namespace ChainingAssertion
{
    /// <summary>reflection helper, access to public/private member with specified name</summary>
    public class ReflectAccessor
    {
        private readonly Type _type;

        /// <summary>target object</summary>
        public object Target { get; }

        /// <summary>get/set member value with specified name</summary>
        public object this[string name]
        {
            get
            {
                var getter = this._type.GetRuntimeProperties()
                    .Where(x => x.Name == name)
                    .Select(x => x.GetMethod)
                    .FirstOrDefault(x => x?.GetParameters().Length == 0);
                if (getter != null)
                    return getter.Invoke(this.Target, null);

                var field = this._type.GetRuntimeFields()
                    .FirstOrDefault(x => x.Name == name);
                if (field != null)
                    return field.GetValue(this.Target);

                throw new ArgumentException($"\"{ name }\" not found : Type <{ this._type.Name }>");
            }
            set
            {
                var setter = this._type.GetRuntimeProperties()
                    .Where(x => x.Name == name)
                    .Select(x => x.SetMethod)
                    .FirstOrDefault(x => x?.GetParameters().Length == 1);
                if (setter != null)
                {
                    setter.Invoke(this.Target, new[] { value });
                    return;
                }

                var field = this._type.GetRuntimeFields()
                    .FirstOrDefault(x => x.Name == name);
                if (field != null)
                {
                    field.SetValue(this.Target, value);
                    return;
                }

                throw new ArgumentException($"\"{ name }\" not found : Type <{ this._type.Name }>");
            }
        }

        /// <summary>create instance with specified target object</summary>
        public ReflectAccessor(object target) : this(target, target.GetType())
        { }

        /// <summary>create instance with specified target object and type</summary>
        public ReflectAccessor(object target, Type type)
        {
            this._type = type;
            this.Target = target;
        }

        /// <summary>get object member value with specified name</summary>
        public static object Reflect(Type type, object target, string name)
            => new ReflectAccessor(target, type)[name];

        /// <summary>get object member value with specified name</summary>
        public static object Reflect(object target, string name)
            => Reflect(target.GetType(), target, name);

        /// <summary>get object member value with specified name</summary>
        public static object Reflect<T>(object target, string name)
            => Reflect(typeof(T), target, name);

        /// <summary>get object member value with specified name</summary>
        public static object Reflect<T>(T target, string name)
            => Reflect(typeof(T), target, name);
    }
}
