using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<Ability> abilities = new List<Ability>();
    [SerializeField] private List<AbilityView> abilityViews = new List<AbilityView>();

    public void Initialize(Character owner)
    {
        this.owner = owner;
        SetupAbilities();
    }

    private void SetupAbilities()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            Ability ability = abilities[i];
            ability.Initialize(owner);

            if (ability is PlayerAbility playerAbility && i < abilityViews.Count)
            {
                playerAbility.SetView(abilityViews[i]);
            }
        }

        foreach (AbilityView abilityView in abilityViews)
        {
            abilityView.Initialize();
        }
    }

    private void Update()
    {
        foreach (Ability ability in abilities)
        {
            ability.HandleInput();
            ability.UpdateCooldown(Time.deltaTime);
            ability.UpdateView();
            ability.UpdateIndicator(playerTransform);
        }
    }

    public void SetAbilities(List<Ability> newAbilities)
    {
        foreach (var ability in abilities)
        {
            ability.Cleanup();
        }

        abilities = newAbilities;

        foreach (var ability in abilities)
        {
            ability.Initialize(owner);
        }
    }

}