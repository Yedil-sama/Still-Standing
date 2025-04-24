using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class AbilityView : IAbilityView
{
    [Header("HUD")]
    [SerializeField] private Image abilityCooldownImage;
    [SerializeField] private TMP_Text abilityText;

    [Header("Indicators")]
    public Image indicatorImage;
    [SerializeField] private RectTransform indicatorTransform;

    public void Initialize()
    {
        SetCooldownFill(0f);
        SetCooldownColor(Color.white);
        SetCooldownText("");
        EnableIndicator(false);
    }

    public void SetCooldownFill(float value)
    {
        if (abilityCooldownImage != null)
            abilityCooldownImage.fillAmount = value;
    }

    public void SetCooldownColor(Color color)
    {
        if (abilityCooldownImage != null)
            abilityCooldownImage.color = color;
    }

    public void SetCooldownText(string text)
    {
        if (abilityText != null)
            abilityText.text = text;
    }

    public void EnableIndicator(bool active)
    {
        if (indicatorImage != null)
        {
            indicatorImage.gameObject.SetActive(active);
            indicatorImage.enabled = active;
        }
    }

    public void UpdateIndicatorRotation(Vector3 from, Vector3 to)
    {
        Vector3 direction = to - from;
        direction.y = 0f;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        indicatorTransform.rotation = Quaternion.Euler(0, angle, 0);
    }
}