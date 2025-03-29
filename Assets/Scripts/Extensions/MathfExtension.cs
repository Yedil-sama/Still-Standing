using UnityEngine;

namespace Extensions
{
    public static class MathfExtension
    {
        public static float Round(this float number, int approximateTo)
        {
            float approximation = Mathf.Pow(10, approximateTo);
            return Mathf.Round(number * approximation) / approximation;
        }
    }

}
