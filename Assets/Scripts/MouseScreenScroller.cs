using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScreenScroller : Singleton<MouseScreenScroller>
{
    public float ScrollSpeed = 1;

    public void Scroll(Vector3 direction)
    {
        direction = direction.normalized;
    
        var startingPos = transform.position;
 
        transform.position = Vector3.Lerp(
            startingPos,
            startingPos + (direction * ScrollSpeed), 
            Time.deltaTime);
    }
}
