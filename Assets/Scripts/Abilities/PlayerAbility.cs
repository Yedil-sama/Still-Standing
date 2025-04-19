using UnityEngine;

public abstract class PlayerAbility : Ability
{
    public KeyCode activationKey;

    public override void HandleInput()
    {
        if (Input.GetKeyDown(activationKey) && CanCast())
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
