public class HealthBarView : StatBarView<Health>
{
    protected override void SubscribeEvents()
    {
        stat.OnTakeDamage += StartLerp;
        stat.OnHealUp += StartLerp;
    }
}
