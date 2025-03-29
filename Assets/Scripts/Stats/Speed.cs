using System;

[Serializable]
public class Speed : Stat
{
    public float minSpeed => GameManager.Instance.minSpeed;

    public float maxSpeed => GameManager.Instance.maxSpeed;
    private Movement movement;
    public override void AddBonus(float amount)
    {
        if (total + amount > maxSpeed)
        {
            amount = maxSpeed - total;
            base.AddBonus(amount);
        }
    }
    public void SetMovementController(Movement movement)
    {
        this.movement = movement;
    }
    public override void OnValidate()
    {
        base.OnValidate();
        if (movement != null) movement.agent.speed = current;
    }

}
