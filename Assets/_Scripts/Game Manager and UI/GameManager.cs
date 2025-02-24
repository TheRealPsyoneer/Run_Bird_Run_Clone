using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static string lastSceneName;
    public PlayerData playerData;
    public GameState gameState;

    public EventSO gameStartEvent;
    public EventSO birdDieEvent;

    public EventSO boxFallCompleteEvent;
    public EventSO scoreChangeEvent;

    public List<ChallengeSO> challenges;

    public List<SpritePseudoList> spriteLists;
    [Serializable]
    public struct SpritePseudoList
    {
        public List<Sprite> sprites;
    }

    public List<int> specialBirdsID;
    public HashSet<int> cachedSpecialBirdsID;

    [Header("Player This Session Data")]
    public int score;

    [Header("Game Difficulty")]
    [SerializeField] int scoreDifficultyThreshold;
    public int ScoreDifficultyThreshold { get; set; }
    public int addedScoreToChangeDifficulty;

    public float fallTimePerCell;
    public float dropSpeedChange;
    public float minSpeed;

    public float waveInterval;
    public float waveIntervalChange;

    public float warningLiveTime;
    public float warningLiveTimeChange;

    [SerializeField] int scoreCandyThreshold;
    public int ScoreCandyThreshold { get; set; }
    public int addedScoreToIncreaseCandy;

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

        cachedSpecialBirdsID = new(specialBirdsID);

        playerData = PlayerData.LoadData();
        if (playerData == null)
        {
            playerData = new();
            playerData.unlockedBirds = new();
            playerData.unlockedThemes = new();
            playerData.isFirstTime = true;
        }

        Application.targetFrameRate = 60;
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
        UpdateNewSkin();

        if (playerData.isFirstTime)
        {
            playerData.unlockedBirds[0] = true;
            playerData.unlockedThemes[0] = true;
            playerData.unlockedThemes[1] = true;
        }
        else
        {
            foreach (int ID in specialBirdsID)
            {
                if (playerData.unlockedBirds[ID])
                {
                    cachedSpecialBirdsID.Remove(ID);
                }
            }
        }

        playerData.SaveData();

        gameState = GameState.Pause;
        lastSceneName = "Main";
    }

    private void UpdateNewSkin()
    {
        if (spriteLists.Count > playerData.unlockedBirds.Count)
        {
            int startIndex = playerData.unlockedBirds.Count;
            for (int i = startIndex; i < spriteLists.Count; i++)
            {
                playerData.unlockedBirds.Add(false);
            }
        }

        if (BackgroundControl.Instance.themeLists.Count > playerData.unlockedThemes.Count)
        {
            int startIndex = playerData.unlockedThemes.Count;
            for (int i = startIndex; i < BackgroundControl.Instance.themeLists.Count; i++)
            {
                playerData.unlockedThemes.Add(false);
            }
        }
    }

    void GameStart()
    {
        gameState = GameState.Playing;

        StartCoroutine(DelayStart());
        
        playerData.gamesPlayed++;
        if (challenges[playerData.challengeNumber].challengeType == ChallengeType.PlayGames)
        {
            playerData.curChallengeProgress++;
        }

        playerData.SaveData();

        score = 0;
        scoreChangeEvent.Broadcast();

        BoxAndCandySpawner.candyNumber = 0;
        BoxBehaviour.FallTimePerCell = fallTimePerCell;
        Warning.LiveTime = warningLiveTime;
        BoxAndCandySpawner.Instance.WaveInterval = waveInterval;
        ScoreDifficultyThreshold = scoreDifficultyThreshold;
        ScoreCandyThreshold = scoreCandyThreshold;

        BoxAndCandySpawner.Instance.InitialCheck();
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1);
        
        BoxAndCandySpawner.Instance.lastWaveDropDone = true;
    }

    void GameOver()
    {
        gameState = GameState.Pause;
        Time.timeScale = 0;

        if (score > playerData.bestScore)
        {
            playerData.bestScore = score;
        }

        if (playerData.curChallengeProgress >= challenges[playerData.challengeNumber].goal)
        {
            ChallengeDetailUI.Instance.ShowCompleteChallengeSequence();

            StartCoroutine(DelayShowEndScreen());
        }
        else
        {
            if (challenges[playerData.challengeNumber].challengeType == ChallengeType.ScoreSingleGame
                || challenges[playerData.challengeNumber].challengeType == ChallengeType.CollectCandiesSingleGame)
            {
                playerData.curChallengeProgress = 0;
            }

            playerData.SaveData();

            MenuUI.Instance.ShowText();
            DeadMenuUI.Instance.ShowMenu();
        }

        
    }

    IEnumerator DelayShowEndScreen()
    {
        yield return new WaitForSecondsRealtime(6.5f);
        MenuUI.Instance.ShowText();
        DeadMenuUI.Instance.ShowMenu();
    }

    void IncreaseScore()
    {
        score++;
        if (challenges[playerData.challengeNumber].challengeType == ChallengeType.ScoreSingleGame
            || challenges[playerData.challengeNumber].challengeType == ChallengeType.ScoreTotalPoints)
        {
            playerData.curChallengeProgress++;
        }
        scoreChangeEvent.Broadcast();
    }

    public void GameRestart()
    {
        gameState = GameState.MainMenu;
        GoToScene("Main");
    }

    void IncreaseDifficulty()
    {
        if (score >= ScoreDifficultyThreshold && BoxBehaviour.FallTimePerCell > minSpeed)
        {
            BoxBehaviour.FallTimePerCell -= dropSpeedChange;
            Warning.LiveTime -= warningLiveTimeChange;
            BoxAndCandySpawner.Instance.WaveInterval -= waveIntervalChange;
            ScoreDifficultyThreshold += addedScoreToChangeDifficulty;
        }
    }

    void DropNewCandy()
    {
        if (score >= ScoreCandyThreshold && BoxAndCandySpawner.candyNumber < BoxAndCandySpawner.CandyFactory.Length-1)
        {
            BoxAndCandySpawner.candyNumber++;
            ScoreCandyThreshold += addedScoreToIncreaseCandy;
        }
    }

    public void GoToScene(string sceneName)
    {
        TransitionUI.Instance.TransitionOut();
        StartCoroutine(LoadScene(sceneName));
    }

    public void ReturnToLastScene()
    {
        TransitionUI.Instance.TransitionOut();
        StartCoroutine(LoadScene(lastSceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        yield return new WaitForSecondsRealtime(TransitionUI.Instance.transitionTime);
        DOTween.KillAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
