using UnityEngine;
using System.Collections;

public static class Extensions {
    /// <summary>
    /// 把颜色转变成16进制的32位格式
    /// </summary>
    /// <param name="c">32位颜色</param>
    /// <returns></returns>
    public static string ToHexString(this Color32 c)
    {
        return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", c.r, c.g, c.b, c.a);
    }

    public static Transform FindRecursively(this Transform transform, string name)
    {
        foreach (var t in transform.GetComponentsInChildren<Transform>(true))
            if (t.name == name)
                return t;
        return null;
    }

}
