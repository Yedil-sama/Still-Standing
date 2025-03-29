using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class Ability
{
    [Header("HUD")]
    [SerializeField] private string abilityName;
    [SerializeField] private Image abilityCooldownImage;
    [SerializeField] private TMP_Text abilityText;
    [SerializeField] private KeyCode activationKey;
    [SerializeField] private float cooldownDuration;
    private float currentCooldown = 0f;
    private bool isOnCooldown = false;

    [Header("Indicators")]
    [SerializeField] private RectTransform indicatorTransform;
    [SerializeField] private Image indicatorImage;
    private bool isIndicatorEnabled = false;

    public void Start()
    {
        indicatorImage.enabled = false;
        abilityCooldownImage.fillAmount = 0;
        abilityText.text = "";
    }
    public void HandleInput()
    {
        if (!isOnCooldown)
        {
            if (Input.GetKeyDown(activationKey))
            {
                EnableIndicator(true);
            }
            if (Input.GetMouseButtonDown(0) && isIndicatorEnabled)
            {
                Activate();
                EnableIndicator(false);
            }
            if (Input.GetMouseButtonDown(1))
            {
                EnableIndicator(false);
            }

        }
    }
    public void UpdateCooldown(float deltaTime)
    {
        if (!isOnCooldown) return;

        currentCooldown -= deltaTime;
        if (currentCooldown <= 0)
        {
            isOnCooldown = false;
            currentCooldown = 0;
            abilityCooldownImage.fillAmount = 0;
            abilityText.text = "";
        }
        else
        {
            abilityCooldownImage.fillAmount = currentCooldown / cooldownDuration;
            abilityText.text = Mathf.Ceil(currentCooldown).ToString();
        }
    }
    public void EnableIndicator(bool action)
    {
        indicatorImage.enabled = action;
        isIndicatorEnabled = action;
        //PlayerInput.isAiming = action;
    }
    private void Activate()
    {
        isOnCooldown = true;
        currentCooldown = cooldownDuration;
        abilityCooldownImage.fillAmount = 1f;
    }
    public void UpdateIndicator(Transform playerTransform)
    {
        Vector3 worldMousePos = Input.mousePosition;
        worldMousePos.z = 10f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(worldMousePos);

        Vector3 direction = worldPosition - playerTransform.position;
        direction.y = 0f;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        indicatorTransform.rotation = Quaternion.Euler(0, angle, 0);
    }


}
