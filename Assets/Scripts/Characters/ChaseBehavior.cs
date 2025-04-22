using UnityEngine;

public class ChaseBehavior : ICharacterBehavior
{
    private Character character;
    private CharacterMovement movement;
    private Transform targetTransform;

    public ChaseBehavior(Character character, CharacterMovement movement, Transform targetTransform)
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
