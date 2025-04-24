using System;

[Serializable]
public class CharacterBrain
{
    public float viewRange = 10f;
    public float waitDuration = 3f;
    protected ICharacterBehavior currentBehavior;
    protected Character owner;
    protected Player player;

    public virtual void Initialize(Character owner)
    {
        this.owner = owner;
        SetBehavior(new WanderBehavior(owner, owner.movement));
        player = GameManager.Instance.player;
    }

    public virtual void Update() => currentBehavior?.Update();

    public void SetBehavior(ICharacterBehavior behavior)
    {
        currentBehavior?.Exit();
        currentBehavior = behavior;
        currentBehavior?.Enter();
    }
}