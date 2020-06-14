using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiController<T> : Singleton<T> where T : MonoBehaviour
{
    public virtual void SetActive(bool active)
    {
        foreach(var module in GetComponentsInChildren<Module>(false))
        {
            module.Active = active;
        }
    }
}
