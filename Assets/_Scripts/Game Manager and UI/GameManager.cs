using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData playerData;
    public GameState gameState;
    public EventSO gameStartEvent;
    public EventSO birdDieEvent;

    

    [Header("Player This Session Data")]
    public int score;
    public EventSO boxFallCompleteEvent;
    public EventSO scoreChangeEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerData = PlayerData.LoadData();
        if (playerData == null)
        {
            playerData = new();
        }
    }

    private void OnEnable()
    {
        gameStartEvent.ThingHappened += GameStart;
        boxFallCompleteEvent.ThingHappened += IncreaseScore;
        birdDieEvent.ThingHappened += GameOver;
    }

    private void OnDisable()
    {
        gameStartEvent.ThingHappened -= GameStart;
        boxFallCompleteEvent.ThingHappened -= IncreaseScore;
        birdDieEvent.ThingHappened -= GameOver;
    }

    void Start()
    {
        gameState = GameState.MainMenu;
    }

    void GameStart()
    {
        StartCoroutine(DelayStart());
        
        playerData.gamesPlayed++;
        playerData.SaveData();

        score = 0;
        scoreChangeEvent.Broadcast();
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1);
        gameState = GameState.Playing;
    }

    void GameOver()
    {
        gameState = GameState.Pause;
        Time.timeScale = 0;
    }

    void IncreaseScore()
    {
        score++;
        scoreChangeEvent.Broadcast();
    }
}
