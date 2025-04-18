using UnityEngine;
using Extensions;
using System;

public class Character : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public Health health;
    protected HealthBarView healthBarView;
    public Mana mana;
    protected ManaBarView manaBarView;
    public Armor armor;
    public Speed speed;

    public Stat attackDamage;
    public AttackSpeed attackSpeed;
    public Stat spellDamage;

    protected Outline outline;
    protected Movement movement;

    public AutoAttack autoAttack;

    public event Action<float> OnApplyDamage;
    public event Action<float> OnTakeDamage;

    public virtual void Initialize()
    {
        outline = GetComponent<Outline>();
        Invoke(nameof(DisableOutline), 0.01f);

        movement = GetComponent<Movement>();
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
        speed.SetMovementController(movement);

        autoAttack = GetComponent<AutoAttack>();

        attackDamage.Start();
        attackSpeed.Start();
        spellDamage.Start();
    }
    public void Start()
    {
        Initialize();
    }

    public virtual void Update()
    {
        health.regeneration.Regenerate(Time.deltaTime);
        mana.regeneration.Regenerate(Time.deltaTime);
    }

    public virtual float ApplyDamage(Damage damage)
    {
        damage.amount = damage.amount * 100 / (100 + armor.Total);

        if (damage.amount < 0)
        {
            damage.amount = 0;
        }

        health.TakeDamage(damage.amount);

        if (health.isDead)
        {
            Die();
        }

        //Debug.Log($"{nameof(gameObject)} took {damage.amount} damage");
        return damage.amount;
    }

    public void Stop() => movement.Stop();

    public void LookAt(Vector3 worldPosition) => movement.LookAt(worldPosition);

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
