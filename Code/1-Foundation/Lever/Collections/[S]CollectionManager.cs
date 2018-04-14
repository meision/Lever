using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Meision.Collections
{
    public static class CollectionManager
    {
        public static string Print<T>(this IList<T> data)
        {
            string text = string.Join(", ", data.MapArray(p => p.ToString()));
            return "[" + text + "]";
        }

        /// <summary>
        /// Add items except exist in list to list.
        /// </summary>
        /// <typeparam name="T">Comparing object.</typeparam>
        /// <param name="origin">origin items</param>
        /// <param name="items">items what want to add to origin.</param>
        public static void Union<T>(IList<T> origin, IList<T> items)
        {
            ThrowHelper.ArgumentNull((origin == null), nameof(origin));
            ThrowHelper.ArgumentNull((items == null), nameof(items));

            // Pick out not exist item.
            foreach (T item in items)
            {
                if (origin.IndexOf(item) < 0)
                {
                    origin.Add(item);
                }
            }
        }

        public static Collection<T> Subtract<T>(Collection<T> left, Collection<T> right)
        {
            ThrowHelper.ArgumentNull((left == null), nameof(left));
            ThrowHelper.ArgumentNull((right == null), nameof(right));

            Collection<T> collection = new Collection<T>();
            foreach (T item in left)
            {
                if (!right.Contains(item))
                {
                    collection.Add(item);
                }
            }
            return collection;
        }

        public static List<T> Subtract<T>(List<T> left, List<T> right)
        {
            ThrowHelper.ArgumentNull((left == null), nameof(left));
            ThrowHelper.ArgumentNull((right == null), nameof(right));

            List<T> collection = new List<T>();
            foreach (T item in left)
            {
                if (!right.Contains(item))
                {
                    collection.Add(item);
                }
            }
            return collection;
        }

        public static void AddRange<T>(this Collection<T> source, IEnumerable<T> items)
        {
            ThrowHelper.ArgumentNull((source == null), nameof(source));
            ThrowHelper.ArgumentNull((items == null), nameof(items));

            foreach (T item in items)
            {
                source.Add(item);
            }
        }

        public static TResult[] MapArray<T, TResult>(this IList<T> source, Func<T, TResult> func)
        {
            ThrowHelper.ArgumentNull((source == null), nameof(source));

            TResult[] destination = new TResult[source.Count];
            if (func != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    destination[i] = func(source[i]);
                }
            }
            return destination;
        }

        public static List<TResult> MapList<T, TResult>(this IList<T> source, Func<T, TResult> func)
        {
            ThrowHelper.ArgumentNull((source == null), nameof(source));

            List<TResult> destination = new List<TResult>(source.Count);
            if (func != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    destination.Add(func(source[i]));
                }
            }
            return destination;
        }

        public static Collection<TResult> MapCollection<T, TResult>(this IList<T> source, Func<T, TResult> func)
        {
            ThrowHelper.ArgumentNull((source == null), nameof(source));

            Collection<TResult> destination = new Collection<TResult>();
            if (func != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    destination.Add(func(source[i]));
                }
            }
            return destination;
        }

        public static TResult[] MakeArray<TResult>(this Vector vector, Func<int, TResult> func)
        {
            ThrowHelper.ArgumentNull((func == null), nameof(func));

            int count = Math.Abs(vector.Length);
            TResult[] array = new TResult[count];
            if (vector.Length > 0)
            {
                int value = vector.Offset;
                for (int i = 0; i < count; i++)
                {
                    array[i] = func(value++);
                }
            }
            else if (vector.Length < 0)
            {
                int value = vector.Offset;
                for (int i = 0; i < count; i++)
                {
                    array[i] = func(value--);
                }
            }
            else
            {
            }
            return array;
        }

        public static TResult[] MakeArray<TResult>(this Range range, Func<int, TResult> func)
        {
            int count = (range.End >= range.Start) ? range.End - range.Start + 1 : range.End - range.Start - 1;
            Vector vector = new Vector(range.Start, count);
            return MakeArray(vector, func);
        }

        public static int GetIndex<TSource>(this IList<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowHelper.ArgumentNull((source == null), nameof(source));
            ThrowHelper.ArgumentNull((predicate == null), nameof(predicate));
            for (int i = 0; i < source.Count; i++)
            {
                if (predicate(source[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int GetIndex<TSource>(this IList<TSource> source, TSource item)
        {
            ThrowHelper.ArgumentNull((source == null), nameof(source));
            ThrowHelper.ArgumentNull((item == null), nameof(item));
            for (int i = 0; i < source.Count; i++)
            {
                if (Object.Equals(source[i], item))
                {
                    return i;
                }
            }

            return -1;
        }


        public static T[] Concatenate<T>(params T[][] parameters)
        {
            int size = 0;
            foreach (T[] items in parameters)
            {
                if (items == null)
                {
                    continue;
                }
                size += items.Length;
            }

            T[] buffer = new T[size];
            int position = 0;
            foreach (T[] items in parameters)
            {
                if (items != null)
                {
                    System.Array.Copy(items, 0, buffer, position, items.Length);
                    position += items.Length;
                }
            }
            return buffer;
        }

        /// <summary>
        /// Divide unitary array to binary array.
        /// </summary>
        /// <param name="array">array which want to be divided</param>
        /// <param name="column">Element count in each dimension</param>
        /// <returns></returns>
        public static T[][] Divide<T>(this IList<T> source, int column)
        {
            // Validate prameters
            ThrowHelper.ArgumentNull((source == null), nameof(source));

            // Array length.
            int length = source.Count;
            // Dimesion to store the array.
            int dimesions = Meision.Algorithms.Calculator.CeilingDivision(length, column);
            // Count;
            int count = 0;

            T[][] matrix = new T[dimesions][];
            if (dimesions > 0)
            {
                for (int i = 0; i < dimesions - 1; i++)
                {
                    matrix[i] = new T[column];
                    // Fill elements
                    for (int j = 0; j < column; j++)
                    {
                        matrix[i][j] = source[count++];
                    }
                }
                // Deal with remainder elements.
                int left = length - (dimesions - 1) * column;
                matrix[dimesions - 1] = new T[left];
                for (int j = 0; j < left; j++)
                {
                    matrix[dimesions - 1][j] = source[count++];
                }
            }

            return matrix;
        }

        public static T[] GetSegment<T>(this T[] array, int offset)
        {
            ThrowHelper.ArgumentNull((array == null), nameof(array));

            return GetSegment(array, offset, array.Length - offset);
        }

        public static T[] GetSegment<T>(this T[] array, int offset, int length)
        {
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            ThrowHelper.ArgumentIndexOutOfRange((offset < 0), nameof(offset));
            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((offset + length) > array.Length));

            T[] buffer = new T[length];
            Array.Copy(array, offset, buffer, 0, length);
            return buffer;
        }

        public static T[] GetSegment<T>(this T[] array, long offset, long length)
        {
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            ThrowHelper.ArgumentIndexOutOfRange((offset < 0), nameof(offset));
            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((offset + length) > array.Length));

            T[] buffer = new T[length];
            Array.Copy(array, offset, buffer, 0, length);
            return buffer;
        }

        public static T[] ToArray<T>(IList<T> list)
        {
            T[] array = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
            return array;
        }

        public static byte[] ToByteArray<T>(T[] array)
        {
            return ToByteArray<T>(array, 0, array.Length);
        }

        public static byte[] ToByteArray<T>(T[] array, int index, int length)
        {
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + length) > array.Length));

            byte[] buffer = new byte[length * Marshal.SizeOf(typeof(T))];
            IntPtr point = Marshal.UnsafeAddrOfPinnedArrayElement(array, index);
            Marshal.Copy(point, buffer, 0, buffer.Length);

            return buffer;
        }

        public static bool Compare<T>(T[] source, T[] destination)
        {
            if (CollectionManager.ReferenceEquals(source, destination))
            {
                return true;
            }
            if (CollectionManager.ReferenceEquals(source, null))
            {
                return false;
            }
            if (CollectionManager.ReferenceEquals(destination, null))
            {
                return false;
            }

            return Compare(source, 0, destination, 0, source.Length);
        }

        public static bool Compare<T>(T[] source, int sourceIndex, T[] destination, int destinationIndex, int length)
        {
            ThrowHelper.ArgumentNull((source == null), nameof(source));
            ThrowHelper.ArgumentIndexOutOfRange((sourceIndex < 0), nameof(sourceIndex));
            ThrowHelper.ArgumentNull((destination == null), nameof(destination));
            ThrowHelper.ArgumentIndexOutOfRange((destinationIndex < 0), nameof(destinationIndex));
            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
            ThrowHelper.ArgumentIndexLengthOutOfArray((((sourceIndex + length) > source.Length) || ((destinationIndex + length) > destination.Length)));

            for (int i = 0; i < length; i++)
            {
                if (!source[sourceIndex + i].Equals(destination[destinationIndex + i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsSingle<T>(this IList<T> source, T item) where T : class
        {
            if (source == null)
            {
                return false;
            }

            return (source.Count == 1) && (source[0] == item);
        }

        public static TResult FirstOrDefaultOfType<TResult>(this System.Collections.IEnumerable source)
        {
            foreach (object item in source)
            {
                if (item is TResult)
                {
                    return (TResult)item;
                }
            }

            return default(TResult);
        }


    }
}
