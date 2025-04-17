using UnityEngine;

public class Enemy : Character
{
    protected CharacterBrain brain;
    public Transform targetTransform;

    public override void Initialize()
    {
        base.Initialize();

        brain = gameObject.AddComponent<CharacterBrain>();
        brain.SetBehavior(new ChaseBehavior(this, movement, targetTransform));
    }

    public override void Update()
    {
        base.Update();

        brain?.Update();
    }
}
