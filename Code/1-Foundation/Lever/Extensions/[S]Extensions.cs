using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static T Alternate<T>(this T item, T compared, T another)
    {
        if (Object.Equals(item, compared))
        {
            return another;
        }

        return item;
    }

    public static TDestination Map<TSource, TDestination>(this TSource item, Func<TSource, TDestination> func)
    {
        return func(item);
    }

    public static TDestination AutoMap<TSource, TDestination>(this TSource item)
        where TDestination : new()
    {
        return Meision.Automatization.Mapper.Map<TSource, TDestination>(item);
    }

    public static void AddIfNotExists<T>(this IList<T> items, T item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
    }
}
