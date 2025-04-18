using UnityEngine;

public interface IAbilityIndicator
{
    void Enable(bool action);
    void UpdateIndicator(Vector3 from, Vector3 to);
}