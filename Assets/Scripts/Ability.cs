using System;
using UnityEngine;

[Serializable]
public class Ability
{
    [Header("Ability Data")]
    [SerializeField] private string abilityName;
    [SerializeField] private KeyCode activationKey;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private float manaCost;

    [Header("View")]
    [SerializeField] private AbilityView abilityView;

    protected float currentCooldown = 0f;
    protected bool isOnCooldown = false;
    protected bool isIndicatorEnabled = false;

    protected Character owner;

    public void Initialize(Character owner)
    {
        this.owner = owner;
        abilityView.Initialize();
    }

    public void HandleInput()
    {
        if (isOnCooldown)
        {
            abilityView.SetCooldownColor(Color.grey);
            return;
        }

        abilityView.SetCooldownColor(Color.white);

        if (Input.GetKeyDown(activationKey))
            EnableIndicator(true);

        if (isIndicatorEnabled)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CancelCast();
                return;
            }

            if (Input.GetMouseButtonDown(0) && CanCast())
            {
                Cast();
            }
        }
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (!isOnCooldown) return;

        currentCooldown -= deltaTime;

        if (currentCooldown <= 0f)
        {
            isOnCooldown = false;
            currentCooldown = 0f;
            abilityView.SetCooldownFill(0f);
            abilityView.SetCooldownColor(Color.white);
            abilityView.SetCooldownText("");
        }
        else
        {
            abilityView.SetCooldownFill(currentCooldown / cooldownDuration);
            abilityView.SetCooldownText(Mathf.Ceil(currentCooldown).ToString());
        }
    }

    public void UpdateIndicator(Transform playerTransform)
    {
        Vector3 worldMousePos = Input.mousePosition;
        worldMousePos.z = 10f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(worldMousePos);
        abilityView.UpdateIndicatorRotation(playerTransform.position, worldPosition);
    }

    protected virtual void Cast()
    {
        owner.mana.Current -= manaCost;
        owner.Stop();
        owner.LookAt(GameManager.Instance.GetMousePosition());

        Activate();
        EnableIndicator(false);
    }

    protected virtual void Activate()
    {
        isOnCooldown = true;
        currentCooldown = cooldownDuration;
        abilityView.SetCooldownFill(1f);
    }

    protected virtual bool CanCast()
    {
        if (owner.mana.Current >= manaCost) return true;

        abilityView.SetCooldownColor(Color.blue);
        abilityView.SetCooldownFill(1f);
        return false;
    }

    protected virtual void CancelCast()
    {
        EnableIndicator(false);
    }

    protected void EnableIndicator(bool active)
    {
        GameManager.Instance.isAbilitySelected = active;
        abilityView.EnableIndicator(active);
        isIndicatorEnabled = active;
    }
}
