public class Resource : Stat, IResource
{
    public virtual void Regenerate(float amount) => Current += amount;
}
