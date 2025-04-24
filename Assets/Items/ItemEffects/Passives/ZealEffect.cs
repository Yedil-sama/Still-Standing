using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "SO/Effects/Passives/Zeal", fileName = "Zeal")]
public class ZealEffect : ItemPassiveEffect
{
    public float moveSpeedBonus = 0.25f;
    public float duration = 0.25f;

    public override void OnAttack(Character owner, Character target)
    {
        owner.StartCoroutine(ApplySpeedBonus(owner));
    }

    private IEnumerator ApplySpeedBonus(Character owner)
    {
        owner.speed.Bonus += moveSpeedBonus;
        yield return new WaitForSeconds(duration);
        owner.speed.Bonus -= moveSpeedBonus;
    }
}
