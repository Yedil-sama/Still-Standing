using System;

public interface IExperienceSystem
{
    void AddExperience(float amount);
    float CurrentExperience { get; }
    int CurrentLevel { get; }
    float ExperienceToNextLevel { get; }
    event Action<int> OnLevelUp;
}
