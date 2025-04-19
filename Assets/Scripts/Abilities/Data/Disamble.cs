using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SO/Player Ability/Disamble", fileName = "Disamble")]
public class Disamble : PlayerAbility
{
    [Header("Disamble Settings")]
    public int requiredStacks = 3;
    public float duration = 4f;
    public float reduceAttackValue = 40f;
    public float stackDuration = 5f;

    private bool isInited = false;
    private Dictionary<Character, int> enemyStacks = new();
    private Dictionary<Character, Coroutine> activeDebuffs = new();
    private Dictionary<Character, Coroutine> stackTimers = new();

    public override void Initialize(Character owner)
    {
        base.Initialize(owner);
        if (!isInited) { isInited = true; return; }

        owner.autoAttack.OnAutoAttackPerformed += HandleAutoAttack;
        isInited = false;
    }


    private void HandleAutoAttack(Character target)
    {
        if (target == null || !(target is Enemy enemy)) return;

        if (!enemyStacks.ContainsKey(enemy))
            enemyStacks[enemy] = 0;

        enemyStacks[enemy]++;

        Debug.Log($"Disamble stack count is {enemyStacks[enemy]}");

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
        if (activeDebuffs.ContainsKey(enemy))
        {
            owner.StopCoroutine(activeDebuffs[enemy]);
        }

        float attackDamageToAddBack = reduceAttackValue;

        enemy.attackDamage.Current -= attackDamageToAddBack;
        Debug.Log("Debuff applied");

        Coroutine debuffCoroutine = owner.StartCoroutine(ResetDebuff(enemy, attackDamageToAddBack));
        activeDebuffs[enemy] = debuffCoroutine;

        yield return debuffCoroutine;
    }

    private IEnumerator ResetDebuff(Enemy enemy, float attackDamageToAddBack)
    {
        yield return new WaitForSeconds(duration);

        enemy.attackDamage.Current += attackDamageToAddBack;

        activeDebuffs.Remove(enemy);
    }
}
