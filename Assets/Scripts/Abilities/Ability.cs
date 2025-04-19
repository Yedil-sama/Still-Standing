using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject, IAbility
{
    [Header("Data")]
    public string abilityName;
    public float cooldownDuration;
    public float manaCost;

    public float baseAmount;
    public float attackDamageScale;
    public float spellDamageScale;

    public float castTime;

    [Header("View")]
    public IAbilityView abilityView;

    protected Character owner;

    [NonSerialized]
    protected float currentCooldown;

    protected bool isOnCooldown;
    protected bool isIndicatorEnabled;

    public virtual void Initialize(Character owner)
    {
        this.owner = owner;
        abilityView?.Initialize();
    }

    public virtual bool CanCast() => !isOnCooldown && owner.mana.Current >= manaCost;

    public virtual void Cast()
    {
        if (!CanCast()) return;

        owner.mana.Current -= manaCost;
        owner.Stop();
        owner.LookAt(GameManager.Instance.GetMousePosition());

        if (castTime > 0f)
        {
            owner.StartCoroutine(DoCast());
        }
        else
        {
            Activate();
        }

        isOnCooldown = true;
        currentCooldown = cooldownDuration;
    }

    private IEnumerator DoCast()
    {
        yield return new WaitForSeconds(castTime);
        Activate();
    }

    public abstract void Activate();

    public virtual void UpdateCooldown(float deltaTime)
    {
        if (!isOnCooldown) return;

        currentCooldown -= deltaTime;

        if (currentCooldown <= 0f)
        {
            currentCooldown = 0f;
            isOnCooldown = false;
        }
    }

    public virtual void UpdateView()
    {
        bool hasEnoughMana = owner.mana.Current >= manaCost;
        bool showFullFill = !isOnCooldown && !hasEnoughMana;

        abilityView?.SetCooldownText(isOnCooldown ? Mathf.Ceil(currentCooldown).ToString() : "");
        abilityView?.SetCooldownFill(isOnCooldown ? currentCooldown / cooldownDuration : showFullFill ? 1f : 0f);
        abilityView?.SetCooldownColor(hasEnoughMana ? Color.gray : Color.blue);
    }
    public virtual void Cleanup() { }

    public virtual void HandleInput() { }

    public virtual void UpdateIndicator(Transform transform)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        abilityView?.UpdateIndicatorRotation(transform.position, worldPos);
    }

    protected void EnableIndicator(bool active)
    {
        GameManager.Instance.isAbilitySelected = active;
        abilityView?.EnableIndicator(active);
        isIndicatorEnabled = active;
    }
}
