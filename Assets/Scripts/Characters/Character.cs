using UnityEngine;
using Extensions;
using System;

public class Character : MonoBehaviour, IDamageable
{
    public Transform rootTransform;

    [Header("Stats")]
    public Health health;
    protected HealthBarView healthBarView;
    public Mana mana;
    protected ManaBarView manaBarView;
    public Armor armor;
    public MagicResistance magicResistance;
    public Speed speed;

    public Stat attackDamage;
    public AttackSpeed attackSpeed;
    public float attackRange;
    public Stat spellDamage;

    protected Outline outline;
    public CharacterMovement movement;
    public AutoAttack autoAttack;

    public float damageAmplification;
    public float damageMultiplication;

    public event Action<Character, StatType> OnStatChanged;

    public event Action<float> OnApplyDamage;
    public event Action<float> OnTakeDamage;

    public event Action<DamageContext> PreDealDamage;
    public event Action<DamageContext> PostDealDamage;

    public virtual void Initialize()
    {
        outline = GetComponent<Outline>();
        Invoke(nameof(DisableOutline), 0.01f);

        autoAttack = GetComponent<AutoAttack>();

        movement = GetComponent<CharacterMovement>();
        movement.stoppingDistance = attackRange;
        speed.SetMovementController(movement);

        health.Start();
        healthBarView = GetComponentInChildren<HealthBarView>();
        healthBarView.Initialize(health);
        health.regeneration.Initialize(health);
        health.regeneration.Start();

        mana.Start();
        manaBarView = GetComponentInChildren<ManaBarView>();
        manaBarView.Initialize(mana);
        mana.regeneration.Initialize(mana);
        mana.regeneration.Start();

        armor.Start();
        speed.Start();
        attackDamage.Start();
        attackSpeed.Start();
        spellDamage.Start();
    }

    public void Start()
    {
        Initialize();
        OnValidate();
    }

    public virtual void Update()
    {
        health.regeneration.Regenerate(Time.deltaTime);
        mana.regeneration.Regenerate(Time.deltaTime);
    }

    public virtual float ApplyDamage(Damage damage)
    {
        if (damage.type == DamageType.Physical)
        {
            damage.amount = damage.amount * 100 / (100 + armor.Current);
        }
        else if (damage.type == DamageType.Magical)
        {
            damage.amount = damage.amount * 100 / (100 + magicResistance.Current);
        }

        damage.amount = Mathf.Max(0f, damage.amount);
        health.TakeDamage(damage.amount);

        if (health.isDead) Die();

        OnTakeDamage?.Invoke(damage.amount);

        Debug.Log($"{gameObject.name} took {damage.amount} damage");
        return damage.amount;
    }

    public void DealDamage(Damage damage, Character target)
    {
        if (target == null) return;

        var context = new DamageContext(damage, target, this);
        PreDealDamage?.Invoke(context);

        float finalAmount = (context.damage.amount + damageAmplification) * (1 + damageMultiplication);
        Damage finalDamage = new Damage(finalAmount, context.damage.type);

        target.ApplyDamage(finalDamage);

        PostDealDamage?.Invoke(context);
        OnApplyDamage?.Invoke(finalAmount);
    }

    public void Stop() => movement.Stop();

    public void LookAt(Vector3 worldPosition) => movement.LookAt(worldPosition);

    public float GetStat(StatType statType) => 
        statType switch
        {
            StatType.Health => health.Current,
            StatType.HealthRegeneration => health.regeneration.Current,
            StatType.Mana => mana.Current,
            StatType.ManaRegeneration => mana.regeneration.Current,
            StatType.Armor => armor.Current,
            StatType.MagicResistance => magicResistance.Current,
            StatType.MovementSpeed => speed.Current,
            StatType.AttackDamage => attackDamage.Current,
            StatType.AttackSpeed => attackSpeed.Current,
            StatType.SpellDamage => spellDamage.Current,
            _ => 0f
        };

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void DisableOutline()
    {
        outline.enabled = false;
    }

    protected void OnValidate()
    {
        health.OnValidate();
        health.regeneration.OnValidate();
        mana.OnValidate();
        mana.regeneration.OnValidate();
        armor.OnValidate();
        speed.OnValidate();
        attackDamage.OnValidate();
        attackSpeed.OnValidate();
        spellDamage.OnValidate();
    }
}
