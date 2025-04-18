using UnityEngine;

public class FollowMouseIndicator : AbilityIndicator
{
    public override void UpdateIndicator(Vector3 from, Vector3 to)
    {
        indicatorTransform.position = to;
    }
}
