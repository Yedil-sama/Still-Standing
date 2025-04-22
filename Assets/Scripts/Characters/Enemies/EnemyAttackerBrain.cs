using System;
using UnityEngine;

[Serializable]
public class EnemyAttackerBrain : CharacterBrain
{
    private AutoAttack autoAttack;

    public override void Initialize(Character owner)
    {
        base.Initialize(owner);
        autoAttack = owner.GetComponent<AutoAttack>();
        Debug.Log("innited");
    }

    public override void Update()
    {
        base.Update();

        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(owner.transform.position, player.transform.position);

        if (distanceToPlayer < viewRange)
        {
            if (!(currentBehavior is ChaseBehavior))
            {
                SetBehavior(new ChaseBehavior(owner, owner.movement, player.transform));
            }

            if (autoAttack != null && distanceToPlayer <= owner.attackRange)
            {
                owner.movement.target = player.gameObject;
                Debug.Log("attacking");
            }

            return;
        }

        if (currentBehavior is ChaseBehavior)
        {
            SetBehavior(new WaitBehavior(owner, waitDuration, () => SetBehavior(new WanderBehavior(owner, owner.movement))));
            return;
        }

        if (autoAttack != null)
        {
            autoAttack.target = null;
        }
    }
}
