using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance;

    public TextMeshProUGUI candy;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI gamesPlayed;
    public TextMeshProUGUI score;

    public CanvasGroup textGroup;

    public EventSO gameStartEvent;
    public EventSO scoreChangeEvent;
    public EventSO birdDieEvent;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        gameStartEvent.ThingHappened += HideMenu;
        gameStartEvent.ThingHappened += ShowScore;
        scoreChangeEvent.ThingHappened += ChangeScoreText;
        birdDieEvent.ThingHappened += HideScore;
    }

    private void OnDisable()
    {
        gameStartEvent.ThingHappened -= HideMenu;
        gameStartEvent.ThingHappened -= ShowScore;
        scoreChangeEvent.ThingHappened -= ChangeScoreText;
        birdDieEvent.ThingHappened -= HideScore;
    }

    void Start()
    {
        if (GameManager.Instance.playerData.gamesPlayed != 0) 
        {
            candy.text = $"{GameManager.Instance.playerData.candy}";
            bestScore.text = $"BEST SCORE: {GameManager.Instance.playerData.bestScore}";
            gamesPlayed.text = $"GAMES PLAYED: {GameManager.Instance.playerData.gamesPlayed}";
        }
        else
        {
            candy.text = "";
            bestScore.text = "";
            gamesPlayed.text = "";
        }
    }

    void HideMenu()
    {
        textGroup.DOFade(0, 1);
    }

    void ShowScore()
    {
        score.gameObject.GetComponent<CanvasGroup>().DOFade(1, 1);
    }

    void HideScore()
    {
        score.gameObject.GetComponent<CanvasGroup>().DOFade(0, 1).SetUpdate(true);
    }

    public void ShowText()
    {
        candy.text = $"{GameManager.Instance.playerData.candy}";
        bestScore.text = $"BEST SCORE: {GameManager.Instance.playerData.bestScore}";
        gamesPlayed.text = $"GAMES PLAYED: {GameManager.Instance.playerData.gamesPlayed}";
        textGroup.DOFade(1, 1).SetUpdate(true);
    }

    void ChangeScoreText()
    {
        score.text = $"{GameManager.Instance.score}";
    }
}
