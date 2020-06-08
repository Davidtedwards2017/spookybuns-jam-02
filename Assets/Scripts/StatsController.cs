using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilites;

public class StatsController : Singleton<StatsController>
{
    private Dictionary<string, int> Flags = new Dictionary<string, int>();
    
    public void ResetFlags()
    {
        Flags.Clear();
    }

    public int GetFlagValue(string name)
    {
        return Flags.SafeGetOrInitialize(name);
    }

    public void SetFlagValue(string name, int value)
    {
        Flags.SafeSet(name, value);
        Debug.Log(string.Format("updating '{0}' with value {1}", name, value));
    }
    
    public void AdjustFlagValue(string name, int adjustment)
    {
        var val = GetFlagValue(name);
        SetFlagValue(name, val + adjustment);
    }
}
