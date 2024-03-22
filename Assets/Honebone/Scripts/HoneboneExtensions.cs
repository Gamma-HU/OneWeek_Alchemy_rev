using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HoneboneExtensions
{
    public static bool CheckRaycastHit(this RaycastHit2D hit, string tagName)
    {
        return hit.collider != null && hit.collider.CompareTag(tagName);
    }
    public static Vector2 UnitCircle(this float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
