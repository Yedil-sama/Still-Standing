public class NPC : Character
{
    protected CharacterBrain brain;

    public override void Initialize()
    {
        base.Initialize();  

        brain = gameObject.AddComponent<CharacterBrain>();
        brain.SetBehavior(new WanderBehavior(this, movement));
    }

    public override void Update() 
    { 
        base.Update(); 

        brain?.Update();
    }
}
