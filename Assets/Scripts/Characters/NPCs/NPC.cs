using UnityEngine;

public class NPC : Character
{
    [SerializeField] protected NPCBrain brain;

    public override void Initialize()
    {
        base.Initialize();  

        brain.Initialize(this);
    }

    public override void Update() 
    { 
        base.Update(); 

        brain?.Update();
    }
}
