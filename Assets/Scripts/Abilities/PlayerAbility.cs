using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player Ability", fileName = "New Ability")]
public class PlayerAbility : Ability
{
    public KeyCode activationKey;

    public override void HandleInput()
    {
        if (isOnCooldown)
        {
            abilityView?.SetCooldownColor(Color.grey);
        }
        else
        {
            abilityView?.SetCooldownColor(Color.white);
        }

        if (Input.GetKeyDown(activationKey))
        {
            EnableIndicator(true);
        }

        if (isIndicatorEnabled)
        {
            if (Input.GetMouseButtonDown(1))
            {
                EnableIndicator(false);
                return;
            }

            if (Input.GetMouseButtonDown(0) && CanCast())
            {
                Cast();
                EnableIndicator(false);
            }
        }
    }


    public override void Activate()
    {
        Debug.Log($"Activated {abilityName}!");
    }

    public void SetView(IAbilityView view)
    {
        this.abilityView = view;
        abilityView.Initialize();
    }


}
