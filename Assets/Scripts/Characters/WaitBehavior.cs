using UnityEngine;
using System;

public class WaitBehavior : ICharacterBehavior
{
    private Character character;
    private float waitTime;
    private float timer;
    private Action onComplete;

    public WaitBehavior(Character character, float waitTime = 3f, Action onComplete = null)
    {
        this.character = character;
        this.waitTime = waitTime;
        this.onComplete = onComplete;
    }

    public void Enter()
    {
        timer = 0;
        character.Stop();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            onComplete?.Invoke();
        }
    }
}