using System;
using System.Linq.Expressions;

namespace Meision.Automatization
{
    public static class Constructor
    {
        //public static T Create<T>(params KeyValuePair<string, object>[] properties)
        //{
        //    Type type = typeof(T);
        //    // Create a instance.
        //    ConstructorInfo info = type.GetConstructor(new Type[0]);
        //    T instance = (T)info.Invoke(null);
        //    // Set Properties
        //    foreach (KeyValuePair<string, object> property in properties)
        //    {
        //        type.GetProperty(property.Key).SetValue(instance, property.Value, null);
        //    }

        //    return instance;
        //}

        //public static T Create<T>(object[] parameters, params KeyValuePair<string, object>[] properties)
        //{
        //    Type type = typeof(T);
        //    // Parameters type
        //    Type[] parameterTypes = new Type[parameters.Length];
        //    for (int i = 0; i < parameters.Length; i++)
        //    {
        //        parameterTypes[i] = parameters[i].GetType();
        //    }

        //    // Create a instance.
        //    ConstructorInfo info = type.GetConstructor(parameterTypes);
        //    T instance = (T)info.Invoke(parameters);
        //    // Set Properties
        //    foreach (KeyValuePair<string, object> property in properties)
        //    {
        //        type.GetProperty(property.Key).SetValue(instance, property.Value, null);
        //    }

        //    return instance;
        //}

        //public static Dictionary<T, K> CreateDictionary<T, K>(params KeyValuePair<T, K>[] items)
        //{
        //    Dictionary<T, K> dictionary = new Dictionary<T, K>();
        //    for (int i = 0; i < items.Length; i++)
        //    {
        //        dictionary.Add(items[i].Key, items[i].Value);
        //    }
        //    return dictionary;
        //}

        public static Func<T> CreateConstructor<T>()
        {
            NewExpression expression = Expression.New(typeof(T));
            return Expression.Lambda<Func<T>>(expression, null).Compile();
        }

        public static Func<object> CreateConstructor(Type type)
        {
            ThrowHelper.ArgumentNull((type == null), nameof(type));

            Expression expression = Expression.New(type);
            if (type.IsValueType)
            {
                expression = Expression.Convert(expression, typeof(object));
            }

            return Expression.Lambda<Func<object>>(expression, null).Compile();
        }
    }
}
