public class DamageContext
{
    public Damage damage;
    public Character target;
    public Character source;

    public DamageContext(Damage damage, Character target, Character source = null)
    {
        this.damage = damage;
        this.target = target;
        this.source = source;
    }
}
