using UnityEngine;


namespace Extensions
{
    public static class Vector2Extensions
    {
        public static float GetRandom(this Vector2 v) => Random.Range(v.x, v.y);

        public static Vector2 GetRoundPosition(this Vector2 value)
        {
            value.x = Mathf.Round(value.x * 100f) / 100f;
            value.y = Mathf.Round(value.y * 100f) / 100f;
            return value;
        }
        
        public static Vector2 GetRandomVector2(this Vector2 vector2) =>
            new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        
        public static Vector2 GetNormalizedRandomVector2(this Vector2 vector2) =>
            GetRandomVector2(vector2).normalized;
    }
}
