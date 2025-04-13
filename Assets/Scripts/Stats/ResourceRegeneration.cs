using System;

[Serializable]
public abstract class ResourceRegeneration<T> : Stat where T : Resource
{
    public float regenInterval;
    private float timer;

    protected T resource;

    public void Initialize(T resource)
    {
        this.resource = resource;
    }

    public void Regenerate(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= regenInterval)
        {
            timer -= regenInterval;

            resource.Current += current;
            resource.ClampCurrent();


        }
    }
}
