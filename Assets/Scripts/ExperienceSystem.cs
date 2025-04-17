using System;

public class ExperienceSystem : IExperienceSystem
{
    private int currentLevel;
    public int CurrentLevel => currentLevel;

    private float currentExperience;
    public float CurrentExperience => currentExperience;

    private ILevelProgression levelProgression;
    private int maxLevel;
    public int MaxLevel => maxLevel;
    public float ExperienceToNextLevel => levelProgression.GetRequiredExpForLevel(currentLevel);

    public event Action<int> OnLevelUp;
    public event Action<float, float> OnExperienceChanged;

    public ExperienceSystem(ILevelProgression progression, float startExp = 0f, int startLevel = 1)
    {
        levelProgression = progression;
        maxLevel = (progression as LevelProgressionData)?.MaxLevel ?? int.MaxValue;

        currentExperience = startExp;
        currentLevel = startLevel;
    }

    public void AddExperience(float amount)
    {
        if (currentLevel >= maxLevel) return;

        currentExperience += amount;

        while (currentLevel < maxLevel && currentExperience >= levelProgression.GetRequiredExpForLevel(currentLevel))
        {
            currentExperience -= levelProgression.GetRequiredExpForLevel(currentLevel++);

            OnLevelUp?.Invoke(currentLevel);
        }

        if (currentLevel >= maxLevel)
        {
            currentExperience = 0f;
        }
        OnExperienceChanged?.Invoke(amount, levelProgression.GetRequiredExpForLevel(currentLevel));
    }
}
