using System;

[Serializable]
public class Speed : Stat
{
    public override float Bonus 
    { 
        get => base.Bonus;
        set
        {
            if (value > maxSpeed)
            {
                value = maxSpeed;
            }
            base.Bonus = value;
        }
    }
    public float minSpeed => GameManager.Instance.minSpeed;
    public float maxSpeed => GameManager.Instance.maxSpeed;
    private Movement movement;
    public void SetMovementController(Movement movement)
    {
        if (movement == null) return;

        movement.Initialize();
        this.movement = movement;
        movement.agent.speed = current;
    }
    public override void OnValidate()
    {
        base.OnValidate();
        if (movement != null) movement.agent.speed = current;
    }

}
