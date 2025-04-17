using UnityEngine;

[CreateAssetMenu(menuName = "SO/Level Progression Data", fileName = "Level Progression Data")]
public class LevelProgressionData : ScriptableObject, ILevelProgression
{
    public float[] requiredExpPerLevel;
    public int MaxLevel => requiredExpPerLevel.Length;

    public float GetRequiredExpForLevel(int level)
    {
        if (level < 1 || level > requiredExpPerLevel.Length)
        {
            return float.MaxValue;
        }
        return requiredExpPerLevel[level - 1];
    }

}
