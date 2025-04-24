using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gold Management")]
    private int playerGold = 0;
    public int PlayerGold
    {
        get => playerGold;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            playerGold = value;
            OnChangeGold?.Invoke(playerGold);
        }
    }
    public int goldIncome = 1;
    public float goldIncomeInterval = 0.5f;
    private float lastGoldTimer = 0f;

    [Header("General")]
    public bool isAbilitySelected = false;
    public Player player;

    public float minSpeed = 0.1f;
    public float maxSpeed = 100f;

    public event Action<int> OnChangeGold;

    [SerializeField] private TMP_Text goldText;

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

        OnChangeGold += UpdateGoldView;

        InitializePlayer();
    }

    public void Update()
    {
        lastGoldTimer += Time.deltaTime;

        if (lastGoldTimer >= goldIncomeInterval)
        {
            lastGoldTimer -= goldIncomeInterval;
            PlayerGold += goldIncome;
        }
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

    private void UpdateGoldView(int amount)
    {
        goldText.text = amount.ToString();
    }

}