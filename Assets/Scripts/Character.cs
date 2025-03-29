using UnityEngine;
using Extensions;

public class Character : MonoBehaviour, IDamageable
{
    public Health health;
    public Armor armor;
    public Speed speed;

    public Stat attackDamage;
    public AttackSpeed attackSpeed;
    public Stat spellDamage;

    protected Movement movement;

    //public List<Stat> stats = new List<Stat>();
    //public virtual void Initialize()
    //{
    //    stats.Clear();

    //    FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

    //    foreach (FieldInfo field in fields)
    //    {
    //        if (field.FieldType.IsSubclassOf(typeof(Stat)) || field.FieldType == typeof(Stat))
    //        {
    //            Stat stat = (Stat)field.GetValue(this);
    //            if (stat != null)
    //            {
    //                stat.Start();
    //                stats.Add(stat);
    //            }
    //        }
    //    }
    //}
    public virtual void Initialize()
    {
        movement = GetComponent<Movement>();
        speed.SetMovementController(movement);

        health.Start();
        armor.Start();
        speed.Start();
        attackDamage.Start();
        attackSpeed.Start();
        spellDamage.Start();
    }
    public void Awake()
    {
        Initialize();
    }

    public virtual float ApplyDamage(Damage damage)
    {
        damage.amount = damage.amount * 100 / (100 + armor.total);

        if (damage.amount < 0)
        {
            damage.amount = 0;
        }

        health.TakeDamage(damage.amount);

        if (health.isDead)
        {
            Die();
        }

        Debug.Log($"{nameof(gameObject)} took {damage.amount} damage");
        return damage.amount;
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    private void OnValidate()
    {
        //foreach (Stat stat in stats)
        //{
        //    stat.OnValidate();
        //}
        health.OnValidate();
        armor.OnValidate();
        speed.OnValidate();
        attackDamage.OnValidate();
        attackSpeed.OnValidate();
        spellDamage.OnValidate();
    }
}
