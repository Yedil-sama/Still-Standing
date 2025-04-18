using UnityEngine;

public interface IAbility
{
    void Initialize(Character owner);
    void Cast();
    void Activate();
    void UpdateCooldown(float deltaTime);
    void HandleInput();
    void UpdateIndicator(Transform transform);
}