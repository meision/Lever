using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public static partial class Extensions
{
    public static string Print<T>(this IEnumerable<T> instance)
    {
        string text = string.Join(",", instance.MapArray(p => p.ToString()));
        return $"[\"{text}\"]";
    }
    
    public static TResult[] MapArray<T, TResult>(this IEnumerable<T> instance, Func<T, TResult> func)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentNull((func == null), nameof(func));

        TResult[] destination = new TResult[instance.Count()];
        int index = 0;
        foreach (T item in instance)
        {
            destination[index++] = func(item);
        }
        return destination;
    }

    public static List<TResult> MapList<T, TResult>(this IEnumerable<T> instance, Func<T, TResult> func)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentNull((func == null), nameof(func));

        List<TResult> destination = new List<TResult>(instance.Count());
        foreach (T item in instance)
        {
            destination.Add(func(item));
        }
        return destination;
    }

    public static Collection<TResult> MapCollection<T, TResult>(this IList<T> instance, Func<T, TResult> func)
    {
        ThrowHelper.ArgumentNull((instance == null), nameof(instance));
        ThrowHelper.ArgumentNull((func == null), nameof(func));

        Collection<TResult> destination = new Collection<TResult>();
        foreach (T item in instance)
        {
            destination.Add(func(item));
        }
        return destination;
    }

}