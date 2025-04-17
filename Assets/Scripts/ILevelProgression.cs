public interface ILevelProgression
{
    int MaxLevel { get; }
    float GetRequiredExpForLevel(int level);
}
