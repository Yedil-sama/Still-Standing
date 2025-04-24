using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Effects/Passives/Witness", fileName = "Witness")]
public class WitnessEffect : ItemPassiveEffect
{
    public float magicResistanceBonus = 5f;
    public float duration = 5f;
    public int maxStacks = 10;

    private int currentStacks = 0;

    public override void OnAttack(Character owner, Character target)
    {
        if (currentStacks < maxStacks)
        {
            owner.StartCoroutine(ApplyMagicResistanceBonus(owner));
        }
    }

    private IEnumerator ApplyMagicResistanceBonus(Character owner)
    {
        currentStacks++;
        owner.speed.Bonus += magicResistanceBonus;

        yield return new WaitForSeconds(duration);

        owner.speed.Bonus -= magicResistanceBonus;
        currentStacks--;
    }
}
