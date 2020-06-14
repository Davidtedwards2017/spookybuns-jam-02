using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationModule : Module
{
    public Animator Animator;

    protected override void OnActivated()
    {
        Animator.SetBool("visible", true);
        base.OnActivated();
    }

    protected override void OnDeactivated()
    {
        Animator.SetBool("visible", false);
        base.OnDeactivated();
    }
}
