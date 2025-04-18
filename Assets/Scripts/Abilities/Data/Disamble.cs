using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SO/Player Ability/Disamble", fileName = "New Disamble")]
public class Disamble : PlayerAbility
{
    [Header("Disamble Settings")]
    public int requiredStacks = 3;
    public float duration = 4f;
    public float reducedAttackValue = 40f;
    public float stackDuration = 2f;

    private Dictionary<Character, int> enemyStacks = new();
    private Dictionary<Character, Coroutine> activeDebuffs = new();
    private Dictionary<Character, Coroutine> stackTimers = new();

    public override void Initialize(Character owner)
    {
        base.Initialize(owner);
        owner.autoAttack.OnAutoAttackPerformed += HandleAutoAttack;
    }

    private void HandleAutoAttack(Character target)
    {
        if (target == null || !(target is Enemy enemy)) return;

        if (!enemyStacks.ContainsKey(enemy))
            enemyStacks[enemy] = 0;

        enemyStacks[enemy]++;

        if (stackTimers.TryGetValue(enemy, out Coroutine stackTimer))
        {
            owner.StopCoroutine(stackTimer);
        }
        stackTimers[enemy] = owner.StartCoroutine(StackDecay(enemy));

        if (enemyStacks[enemy] >= requiredStacks)
        {
            enemyStacks[enemy] = 0;

            if (activeDebuffs.TryGetValue(enemy, out Coroutine debuff))
            {
                owner.StopCoroutine(debuff);
            }

            Coroutine newDebuff = owner.StartCoroutine(DoDisamble(enemy));
            activeDebuffs[enemy] = newDebuff;

            if (stackTimers.TryGetValue(enemy, out Coroutine timer))
            {
                owner.StopCoroutine(timer);
                stackTimers.Remove(enemy);
            }
        }
    }

    private IEnumerator StackDecay(Enemy enemy)
    {
        yield return new WaitForSeconds(stackDuration);

        if (enemyStacks.TryGetValue(enemy, out int stack) && stack > 0)
        {
            enemyStacks[enemy]--;
            if (enemyStacks[enemy] > 0)
            {
                stackTimers[enemy] = owner.StartCoroutine(StackDecay(enemy));
            }
            else
            {
                stackTimers.Remove(enemy);
                enemyStacks.Remove(enemy);
            }
        }
    }

    private IEnumerator DoDisamble(Enemy enemy)
    {
        float originalAttack = enemy.attackDamage.Current;
        enemy.attackDamage.Current = reducedAttackValue;

        yield return new WaitForSeconds(duration);

        enemy.attackDamage.Current = originalAttack;
        activeDebuffs.Remove(enemy);
    }
}
