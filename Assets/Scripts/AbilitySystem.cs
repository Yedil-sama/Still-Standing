using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Canvas abilitiesCanvas;
    [SerializeField] private List<Ability> abilities = new List<Ability>();

    public void Awake()
    {
        owner = GetComponent<Character>();
    }
    public void Start()
    {
        foreach (Ability ability in abilities)
        {
            ability.Initialize(owner);
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