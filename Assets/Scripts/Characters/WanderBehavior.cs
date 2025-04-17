using UnityEngine;

public class WanderBehavior : ICharacterBehavior
{
    private Character character;
    private Movement movement;
    private float wanderTime = 0;

    public WanderBehavior(Character character, Movement movement, float wanderTime = 5f)
    {
        this.character = character;
        this.movement = movement;
        this.wanderTime = wanderTime;
    }

    public void Update()
    {
        wanderTime -= Time.deltaTime;
        if (wanderTime <= 0)
        {
            Vector3 randomDestination = character.transform.position + Random.insideUnitSphere * 5;
            randomDestination.y = character.transform.position.y;

            movement.Move(randomDestination);
            wanderTime = Random.Range(2f, 5f);
        }
    }
}
