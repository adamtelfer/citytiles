using UnityEngine;
using System.Collections;

public class DebugUtils {
    public static void Assert(bool condition, string reason)
    {
        if (!condition)
        {
            throw new System.Exception(reason);
        }
    }
}
