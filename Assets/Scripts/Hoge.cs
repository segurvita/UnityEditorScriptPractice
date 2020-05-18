using System;
using UnityEngine;

[Serializable]
public class Hoge : ScriptableObject
{
    [SerializeField]
    private int intValue;

    public int IntValue
    {
        get { return intValue; }
#if UNITY_EDITOR
        set { intValue = Mathf.Clamp(value, 0, int.MaxValue); }
#endif
    }
}
