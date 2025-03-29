using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        //InitializePlayer();
    }
    //public void InitializePlayer()
    //{
    //    player.Initialize();
    //}

}