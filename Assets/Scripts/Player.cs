using UnityEngine;

public class Player : Character
{
    [SerializeField] private AbilitySystem abilitySystem;
    [SerializeField] private AutoAttack autoAttack;
    public override void Initialize()
    {
        base.Initialize();
        abilitySystem = GetComponent<AbilitySystem>();
        autoAttack = GetComponent<AutoAttack>();

        //base.Initialize();

        speed.SetMovementController(movement);
    }
    public void Start()
    {
        movement.agent.speed = speed.current;
    }
}
