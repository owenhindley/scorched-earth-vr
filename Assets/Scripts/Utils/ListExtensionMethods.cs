using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ListExtensionMethods
{
    public static T First<T>(this List<T> list)
    { 
        if (list.Count > 0)
        {
            return list[0];
        }
        return default(T);
    }

    public static T Last<T>(this List<T> list)
    {
        if(list.Count > 0)
        {
            return list[list.Count - 1];
        }
        return default(T);
    }

    public static int LastIndex<T>(this List<T> list)
    {
        return list.Count - 1;
    }
}
