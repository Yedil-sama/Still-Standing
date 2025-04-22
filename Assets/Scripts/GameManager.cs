using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerGold = 0;
    public bool isAbilitySelected = false;
    public Player player;

    public float minSpeed = 0.1f;
    public float maxSpeed = 100f;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        InitializePlayer();
    }
    public void InitializePlayer()
    {
        player.Initialize();
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePos.y = player.transform.position.y;

        return worldMousePos;
    }

}