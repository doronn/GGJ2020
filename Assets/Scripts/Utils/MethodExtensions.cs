using UnityEngine;

namespace Utils
{
    public static class MethodExtensions
    {
        public static bool TryGetComponentInParent<T>(this GameObject obj, out T component)
        {
            component = obj.GetComponentInParent<T>();

            return component != null;
        }
    }
}