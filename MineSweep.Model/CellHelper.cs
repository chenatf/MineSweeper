using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MineSweep.Model
{
    internal static class CellGenerator
    {
        public static void Shuffle<T>(IList<T> lst)
        {
            var rand = new Random();
            var count = lst.Count;
            for(int i = 0; i < count; ++i)
            {
                var k = i + (rand.Next() % (count - i));
                var tmp = lst[k];
                lst[k] = lst[i];
                lst[i] = tmp;
            }
        }



        public static void Deconstruct<T1, T2, V>(this KeyValuePair<(T1, T2), V> kvp, out T1 item1, out T2 item2, out V value)
        {
            (item1, item2) = kvp.Key;
            value = kvp.Value;
        }
    }
}
