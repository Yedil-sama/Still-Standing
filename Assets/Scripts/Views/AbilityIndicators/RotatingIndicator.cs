using UnityEngine;

public class RotatingIndicator : AbilityIndicator
{
    public override void UpdateIndicator(Vector3 from, Vector3 to)
    {
        Vector3 dir = to - from;
        dir.y = 0;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        indicatorTransform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
