using UnityEngine;

public static class TransformExtensions
{
    public static void DestroyChildren(this Transform transform)
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
            Object.Destroy(transform.GetChild(i).gameObject);
    }
    
    public static void DestroyChildAt(this Transform transform, int i)
    {
        Object.Destroy(transform.GetChild(i).gameObject);
    }

    public static void ResetTransformation(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
