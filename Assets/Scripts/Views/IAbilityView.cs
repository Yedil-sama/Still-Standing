using UnityEngine;

public interface IAbilityView
{
    void Initialize();
    void SetCooldownFill(float value);
    void SetCooldownText(string text);
    void SetCooldownColor(Color color);
    void EnableIndicator(bool enable);
    void UpdateIndicatorRotation(Vector3 origin, Vector3 target);
}
