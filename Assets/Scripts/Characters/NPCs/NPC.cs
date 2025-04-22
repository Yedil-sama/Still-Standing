using UnityEngine;

public class NPC : Character
{
    [SerializeField] protected NPCBrain brain;

    public override void Initialize()
    {
        base.Initialize();  

        brain.Initialize(this);
        brain.SetBehavior(new WanderBehavior(this, movement));
    }

    public override void Update() 
    { 
        base.Update(); 

        brain?.Update();
    }
}
