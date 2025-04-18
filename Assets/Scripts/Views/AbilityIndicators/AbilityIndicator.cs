using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityIndicator : MonoBehaviour, IAbilityIndicator
{
    [SerializeField] protected Transform indicatorTransform;
    [SerializeField] protected Image indicatorImage;

    public virtual void Enable(bool action) => indicatorImage.enabled = action;

    public abstract void UpdateIndicator(Vector3 from, Vector3 to);
}
