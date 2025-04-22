using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected CharacterBrain brain;

    public override void Initialize()
    {
        base.Initialize();

        brain = new EnemyAttackerBrain();
        brain.Initialize(this);

        brain.SetBehavior(new WanderBehavior(this, movement));
    }

    public override void Update()
    {
        base.Update();

        brain?.Update();
    }
}
