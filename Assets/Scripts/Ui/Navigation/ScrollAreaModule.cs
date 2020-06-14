using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollAreaModule : Module, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 ScrollDirection;
    public bool hover = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
    }

    protected override void OnDeactivated()
    {
        hover = false;
        base.OnDeactivated();
    }

    private void Update()
    {
        if(Active && hover)
        {
            MouseScreenScroller.Instance.Scroll(ScrollDirection);
        }
    }
}
