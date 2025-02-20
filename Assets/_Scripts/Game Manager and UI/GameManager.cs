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

    [Header("Game Difficulty")]
    public int scoreDifficultyThreshold;
    public int addedScoreToChangeDifficulty;

    public float fallTimePerCell;
    public float dropSpeedChange;
    public float minSpeed;


    public float warningLiveTime;
    public float warningLiveTimeChange;

    public int scoreCandyThreshold;
    public int addedScoreToIncreaseCandy;

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
        scoreChangeEvent.ThingHappened += IncreaseDifficulty;
        scoreChangeEvent.ThingHappened += DropNewCandy;
    }

    private void OnDisable()
    {
        gameStartEvent.ThingHappened -= GameStart;
        boxFallCompleteEvent.ThingHappened -= IncreaseScore;
        birdDieEvent.ThingHappened -= GameOver;
        scoreChangeEvent.ThingHappened -= IncreaseDifficulty;
        scoreChangeEvent.ThingHappened -= DropNewCandy;
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

        BoxAndCandySpawner.candyNumber = 0;
        BoxBehaviour.FallTimePerCell = fallTimePerCell;
        Warning.LiveTime = warningLiveTime;

        BoxAndCandySpawner.Instance.InitialCheck();
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
        }

        playerData.SaveData();

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

    void IncreaseDifficulty()
    {
        if (score >= scoreDifficultyThreshold && BoxBehaviour.FallTimePerCell > minSpeed)
        {
            BoxBehaviour.FallTimePerCell -= dropSpeedChange;
            Warning.LiveTime -= warningLiveTimeChange;
            scoreDifficultyThreshold += addedScoreToChangeDifficulty;
        }
    }

    void DropNewCandy()
    {
        if (score >= scoreCandyThreshold && BoxAndCandySpawner.candyNumber < BoxAndCandySpawner.CandyFactory.Length-1)
        {
            BoxAndCandySpawner.candyNumber++;
            scoreCandyThreshold += addedScoreToIncreaseCandy;
        }
    }
}
