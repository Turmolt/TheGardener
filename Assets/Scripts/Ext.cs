using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ext
{
    public static Vector3 xyz(this Vector2 _v)
    {
        return new Vector3(_v.x,_v.y,0f);
    }
    
    public static Vector2 xy(this Vector3 _v)
    {
        return new Vector2(_v.x,_v.y);
    }

    public static float AddClamped(this float _f, float add,float min, float max)
    {
        var _float = _f + add;
        return Mathf.Clamp(_float, min, max);
    }
}
