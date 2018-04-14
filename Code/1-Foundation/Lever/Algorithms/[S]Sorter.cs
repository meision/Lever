using System;
using System.Collections.Generic;

namespace Meision.Algorithms
{
    public static class Sorter
    {
        public static void QuickSort<T>(IList<T> items) where T : IComparable<T>
        {
            QuickSort(items, 0, items.Count - 1);
        }

        public static void QuickSort<T>(IList<T> items, int lower, int upper) where T : IComparable<T>
        {
            if (lower >= upper)
            {
                return;
            }

            T pivot = items[lower];
            int i = lower - 1;
            int j = upper + 1;

            while (true)
            {
                while (items[++i].CompareTo(pivot) < 0)
                {
                }
                while (items[--j].CompareTo(pivot) > 0)
                {
                }

                if (i >= j) break;

                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }

            QuickSort(items, lower, i - 1);
            QuickSort(items, j + 1, upper);
        }
    }
}
