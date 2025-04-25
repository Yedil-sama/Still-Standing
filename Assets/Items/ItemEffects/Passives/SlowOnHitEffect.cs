using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Effects/Passives/SlowOnHit", fileName = "SlowOnHit")]
public class SlowOnHitEffect : ItemPassiveEffect
{
    public float slowAmountPerStack = 0.1f;
    public float slowDuration = 2f;
    public int maxStacks = 3;

    private Dictionary<Character, int> slowStacks = new();

    public override void OnAttack(Character owner, Character target)
    {
        if (target == null) return;

        if (!slowStacks.ContainsKey(target))
            slowStacks[target] = 0;

        if (slowStacks[target] < maxStacks)
        {
            slowStacks[target]++;
            target.speed.Bonus -= slowAmountPerStack;
        }

        owner.StartCoroutine(RemoveSlowStack(target));
    }

    private IEnumerator RemoveSlowStack(Character target)
    {
        yield return new WaitForSeconds(slowDuration);

        if (target != null && slowStacks.ContainsKey(target) && slowStacks[target] > 0)
        {
            slowStacks[target]--;
            target.speed.Bonus += slowAmountPerStack;

            if (slowStacks[target] == 0)
                slowStacks.Remove(target);
        }
    }
}
