using UnityEngine;

public class WanderBehavior : ICharacterBehavior
{
    private Character character;
    private CharacterMovement movement;
    private float wanderRadius;
    private float wanderInterval;
    private float wanderTimer = 0;

    public WanderBehavior(Character character, CharacterMovement movement, float wanderRadius = 10f, float wanderInterval = 3f)
    {
        this.character = character;
        this.movement = movement;
        this.wanderRadius = wanderRadius;
        this.wanderInterval = wanderInterval;
    }

    public void Enter() => wanderTimer = wanderInterval;

    public void Update()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection.y = 0;
            Vector3 destination = character.transform.position + randomDirection;

            movement.Move(destination);
            wanderTimer = wanderInterval;
        }
    }

    public void Exit() => movement.Stop();
}
