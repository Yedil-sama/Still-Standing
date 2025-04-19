using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player Ability/Execution Protocol", fileName = "Execution Protocol")]
public class ExecutionProtocol : PlayerAbility
{
    [Header("Execution Protocol Settings")]
    public float threshold;
    public float missingHealthMultiplier;

    private bool isInited = false;

    public override void Initialize(Character owner)
    {
        base.Initialize(owner);
        if (!isInited) { isInited = true; return; }

        owner.PreDealDamage += OnPreDealDamage;
        isInited = false;
    }

    private void OnPreDealDamage(DamageContext context)
    {
        if (context.target == null || context.damage.amount <= 0f) return;

        float targetHealthRatio = context.target.health.Current / context.target.health.Total;

        if (targetHealthRatio < threshold)
        {
            float old = context.damage.amount;
            float scale = 1f + ((1f - targetHealthRatio) * missingHealthMultiplier);
            context.damage.amount *= scale;
            Debug.Log($"premitigation damage was buffed to {context.damage.amount - old}");
        }
    }

    public override void Cleanup()
    {
        if (owner != null)
        {
            owner.PreDealDamage -= OnPreDealDamage;
        }
    }
}
