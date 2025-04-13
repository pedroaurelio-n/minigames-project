using System;
using System.Collections.Generic;

public static class EnumerableExtensions
{
    public static void DisposeAndClear<T> (this IEnumerable<T> collection) where T : PoolableView
    {
        foreach (T obj in collection)
            obj.Despawn();

        switch (collection)
        {
            case List<T> list:
                list.Clear();
                break;
            case HashSet<T> hashSet:
                hashSet.Clear();
                break;
            case Queue<T> queue:
                queue.Clear();
                break;
            case Stack<T> stack:
                stack.Clear();
                break;
            case T[] array:
                Array.Clear(array, 0, array.Length);
                break;
            default:
                DebugUtils.LogWarning($"Trying to clear unsupported collection type: {collection.GetType()}");
                break;
        }
    }
}