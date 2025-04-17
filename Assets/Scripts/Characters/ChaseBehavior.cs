using UnityEngine;

public class ChaseBehavior : ICharacterBehavior
{
    private Character character;
    private Movement movement;
    private Transform targetTransform;

    public ChaseBehavior(Character character, Movement movement, Transform targetTransform)
    {
        this.character = character;
        this.movement = movement;
        this.targetTransform = targetTransform;
    }

    public void Update()
    {
        if (targetTransform == null) return;

        movement.Move(targetTransform.position);
        character.LookAt(targetTransform.position);
    }

    public void Exit() => movement.Stop();
}
