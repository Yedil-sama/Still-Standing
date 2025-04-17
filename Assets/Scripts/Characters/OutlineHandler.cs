using UnityEngine;

public class OutlineHandler : MonoBehaviour
{
    [SerializeField] private float delay = 0.01f;
    private Outline outline;
    public void Start()
    {
        outline = GetComponent<Outline>();
        Invoke(nameof(EnableOutline), delay);
    }
    private void EnableOutline() => outline.enabled = true;

    private void DisableOutline() => outline.enabled = false;
    
}