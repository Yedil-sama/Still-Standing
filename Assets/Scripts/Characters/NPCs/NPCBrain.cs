using System;
using UnityEngine;

[Serializable]
public class NPCBrain : CharacterBrain
{
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
            return;
        }

        if (currentBehavior is ChaseBehavior)
        {
            SetBehavior(new WaitBehavior(owner, waitDuration, () => SetBehavior(new WanderBehavior(owner, owner.movement))));
            return;
        }

    }
}