using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPiece : MonoBehaviour
{
    public SpriteRenderer Renderer;

    public float Width()
    {
        return Renderer.bounds.size.x;
    }

    public Vector3 GetLeftPoint()
    {
        return transform.position - new Vector3(Renderer.bounds.extents.x, 0, 0);
    }

    public Vector3 GetRightPoint()
    {
        return transform.position + new Vector3(Renderer.bounds.extents.x, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(GetLeftPoint(), GetRightPoint());
    }
}
