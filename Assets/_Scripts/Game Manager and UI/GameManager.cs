using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData playerData;
    public GameState gameState;
    public DeadMenuUI deadMenuUI;

    public EventSO gameStartEvent;
    public EventSO birdDieEvent;

    public EventSO boxFallCompleteEvent;
    public EventSO scoreChangeEvent;

    [Header("Player This Session Data")]
    public int score;
    

    private void Awake()
    {
        Instance = this;

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

        if (score > playerData.bestScore)
        {
            playerData.bestScore = score;
            playerData.SaveData();
        }

        deadMenuUI.ShowMenu();
    }

    void IncreaseScore()
    {
        score++;
        scoreChangeEvent.Broadcast();
    }

    public void GameRestart()
    {
        DOTween.KillAll();
        Time.timeScale = 1;
        gameState = GameState.MainMenu;
        SceneManager.LoadScene(0);
    }
}
