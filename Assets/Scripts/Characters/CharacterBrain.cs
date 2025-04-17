using UnityEngine;

public class CharacterBrain : MonoBehaviour
{
    private ICharacterBehavior currentBehavior;

    public void SetBehavior(ICharacterBehavior behavior)
    {
        currentBehavior?.Exit();
        currentBehavior = behavior;
        currentBehavior?.Enter();
    }

    public void Update() => currentBehavior?.Update();
}
