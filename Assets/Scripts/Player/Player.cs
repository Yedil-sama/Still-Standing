using UnityEngine;

public class Player : Character
{
    public HealthBarView interfaceHealthBarView;
    public ManaBarView interfaceManaBarView;
    [SerializeField] private AbilitySystem abilitySystem;
    [SerializeField] private MeleeAutoAttack autoAttack;
    public override void Initialize()
    {
        base.Initialize();
        abilitySystem = GetComponent<AbilitySystem>();
        autoAttack = GetComponent<MeleeAutoAttack>();

        interfaceHealthBarView.Initialize(health);
        interfaceManaBarView.Initialize(mana);
        speed.SetMovementController(movement);
    }
    public void Start()
    {
        movement.agent.speed = speed.Current;
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.V))
        {
            ApplyDamage(new Damage(5));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            mana.Spend(5);
        }
    }
}
