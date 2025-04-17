public class LinearProgression : ILevelProgression
{
    public int MaxLevel => maxLevel;
    private int maxLevel;
    private float baseExp;
    private float growth;

    public LinearProgression(float baseExp, float growth, int maxLevel = 18)
    {
        this.baseExp = baseExp;
        this.growth = growth;
        this.maxLevel = maxLevel;
    }

    public float GetRequiredExpForLevel(int level) => baseExp + growth * (level - 1);
}
