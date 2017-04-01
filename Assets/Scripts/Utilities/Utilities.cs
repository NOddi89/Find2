using System;
using System.Collections;
using System.Collections.Generic;

public static class Utilities
{

    /// <summary>
    /// Shuffeling items in list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="rnd"></param>
    public static void Shuffle<T>(this IList<T> list, Random rnd)
    {
        for(var i = 0; i < list.Count; i++)
        {
            list.Swap(i, rnd.Next(i, list.Count));
        }
    }

    /// <summary>
    /// Swaping swaping two items in the used list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
