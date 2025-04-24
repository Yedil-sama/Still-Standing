//using System;
//using UnityEngine;

//[Serializable]
//public class DavidBrain : NPCBrain
//{
//    private float playerChaseTimer = 0f;
//    private bool isChasingPlayer = false;

//    public override void Update()
//    {
//        base.Update();

//        float distanceToPlayer = Vector3.Distance(owner.transform.position, player.transform.position);

//        if (distanceToPlayer < viewRange && !isChasingPlayer)
//        {
//            isChasingPlayer = true;
//            playerChaseTimer = 5f;
//            SetBehavior(new ChaseBehavior(owner, owner.movement, player.transform));
//            return;
//        }

//        if (isChasingPlayer)
//        {
//            playerChaseTimer -= Time.deltaTime;
//            if (playerChaseTimer <= 0f)
//            {
//                isChasingPlayer = false;
//                SetBehavior(new WaitBehavior(owner, waitDuration, StartWanderingCycle));
//            }
//            return;
//        }

//        Character enemy = FindNearestEnemy();
//        if (enemy != null)
//        {
//            if (!(currentBehavior is ChaseBehavior))
//            {
//                SetBehavior(new ChaseBehavior(owner, owner.movement, enemy.transform));
//            }
//            return;
//        }

//        if (!(currentBehavior is WanderBehavior) && !(currentBehavior is WaitBehavior))
//        {
//            StartWanderingCycle();
//        }
//    }

//    private void StartWanderingCycle()
//    {
//        if (currentBehavior is WanderBehavior) return;

//        SetBehavior(new WanderBehavior(owner, owner.movement, OnDestinationReached));
//    }

//    private void OnDestinationReached()
//    {
//        float waitTime = UnityEngine.Random.Range(1f, waitDuration);
//        SetBehavior(new WaitBehavior(owner, waitTime, () =>
//        {
//            StartWanderingCycle();
//        }));
//    }

//    private Character FindNearestEnemy()
//    {
//        Collider[] hits = Physics.OverlapSphere(owner.transform.position, viewRange);
//        Character nearest = null;
//        float shortestDistance = Mathf.Infinity;

//        foreach (Collider hit in hits)
//        {
//            if (hit.TryGetComponent<Character>(out Character character) && character != owner && !(character is Player))
//            {
//                float dist = Vector3.Distance(owner.transform.position, character.transform.position);
//                if (dist < shortestDistance)
//                {
//                    shortestDistance = dist;
//                    nearest = character;
//                }
//            }
//        }

//        return nearest;
//    }
//}
