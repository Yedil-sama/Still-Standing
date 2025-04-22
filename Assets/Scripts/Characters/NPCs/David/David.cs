using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class David : NPC
{
    public override void Initialize()
    {
        brain = new DavidBrain();

        base.Initialize();

    }
}
