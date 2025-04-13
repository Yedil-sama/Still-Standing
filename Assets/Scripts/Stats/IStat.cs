public interface IStat
{
    float Current { get; set; }
    float BaseValue { get; set; }
    float Bonus { get; set; }
    float Total { get; set; }
    void ClampCurrent();
}