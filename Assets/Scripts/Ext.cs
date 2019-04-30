using System.Collections;
using System.Collections.Generic;
using BackwardsCap;
using TMPro;
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

    public static BodyPartModel AsBodyPart(this ObjectModel _o)
    {
        return (BodyPartModel) _o;
    }

    public static BodyPart AsBodyPart(this GrabbableObject _g)
    {
        return (BodyPart) _g;
    }

    public static float AddClamped(this float _f, float add,float min, float max)
    {
        var _float = _f + add;
        return Mathf.Clamp(_float, min, max);
    }
    
    public static IEnumerator FillText(this string newText, TextMeshProUGUI display, float delay = 0.15f)
    {

        int i = 0;
        string s = "";
        while (s !=newText)
        {
            s = newText.Substring(0, i++);
            display.text = s;
            yield return new WaitForSeconds(delay);
                    
                    
        }
        yield return 0;
    }
}
