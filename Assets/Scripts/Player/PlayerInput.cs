//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerInput : MonoBehaviour
//{
//    public static bool isAiming = false;

//    [SerializeField] private PlayerMovement playerMovement;
//    [SerializeField] private AbilitySystem abilitySystem;

//    private bool isMovementEnabled = true;
//    private bool isAbilityEnabled = true;

//    private void Update()
//    {
//        if (isMovementEnabled)
//        {
//            HandleMovementInput();
//        }

//        if (isAbilityEnabled)
//        {
//            HandleAbilityInput();
//        }
//    }

//    private void HandleMovementInput()
//    {
//        if (Input.GetMouseButtonDown(1))
//        {
//            if (isAiming)
//            {
//                isAiming = false;
//                return;
//            }
//            playerMovement.HandleInput();
//        }
//    }

//    private void HandleAbilityInput()
//    {
//        abilitySystem.HandleInput();
//    }

//    public void EnableMovement(bool action)
//    {
//        isMovementEnabled = action;
//        playerMovement.enabled = action;
//    }

//    public void EnableAbilities(bool action)
//    {
//        isAbilityEnabled = action;
//        abilitySystem.enabled = action;
//    }

//}
