using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBarView : MonoBehaviour
{
    [SerializeField] private TMP_Text gameLevelText;
    [SerializeField] private TMP_Text currentLevelText;
    [SerializeField] private TMP_Text nextLevelText;
    [SerializeField] private Image valueImage;
    [SerializeField] private ExperienceSystem experienceSystem;

    public void Initialize(ExperienceSystem experienceSystem)
    {
        this.experienceSystem = experienceSystem;
        OnEnable();
        UpdateView();
    }

    public void OnEnable()
    {
        if (experienceSystem == null) return;

        experienceSystem.OnLevelUp += HandleLevelUp;
        experienceSystem.OnExperienceChanged += HandleExperienceChanged;
    }

    public void OnDisable()
    {
        if (experienceSystem == null) return;

        experienceSystem.OnLevelUp -= HandleLevelUp;
        experienceSystem.OnExperienceChanged -= HandleExperienceChanged;
    }

    private void HandleLevelUp(int newLevel) => UpdateView();

    private void HandleExperienceChanged(float currentExp, float expToNext) => UpdateView();

    private void UpdateView()
    {
        int currLevel = experienceSystem.CurrentLevel;
        currentLevelText.text = currLevel.ToString();

        nextLevelText.text = currLevel < experienceSystem.MaxLevel ? (currLevel + 1).ToString() : "-";

        gameLevelText.text = currLevel.ToString();

        float required = experienceSystem.ExperienceToNextLevel;
        float currentExp = experienceSystem.CurrentExperience;
        float progress = required > 0f ? currentExp / required : 1f;
        valueImage.fillAmount = Mathf.Clamp01(progress);
    }
}
