using UnityEngine;

public class Player : Character
{
    [Header("Dependencies")]
    public HealthBarView interfaceHealthBarView;
    public ManaBarView interfaceManaBarView;
    public ExperienceBarView experienceBarView;
    public LevelProgressionData levelProgressionData;

    [SerializeField] private AbilitySystem abilitySystem;
    [SerializeField] private ExperienceSystem experienceSystem;

    public override void Initialize()
    {
        base.Initialize();
        abilitySystem = GetComponent<AbilitySystem>();

        interfaceHealthBarView.Initialize(health);
        interfaceManaBarView.Initialize(mana);
        experienceBarView.Initialize(experienceSystem = new ExperienceSystem(levelProgressionData));

        abilitySystem.Initialize(this);
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            experienceSystem.AddExperience(5);
        }
    }
}
