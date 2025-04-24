public interface ICharacterBrain
{
    void Initialize(Character owner);
    void Update();
    void SetBehavior(ICharacterBehavior behavior);
}
