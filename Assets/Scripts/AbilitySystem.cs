using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Canvas abilitiesCanvas;
    [SerializeField] private List<Ability> abilities = new List<Ability>();


    public void Start()
    {
        foreach (Ability ability in abilities)
        {
            ability.Start();
        }
    }
    public void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        foreach (Ability ability in abilities)
        {
            ability.HandleInput();
            ability.UpdateCooldown(Time.deltaTime);
            ability.UpdateIndicator(playerTransform);
        }
    }
}