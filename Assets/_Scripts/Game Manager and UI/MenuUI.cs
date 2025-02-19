using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MenuUI : MonoBehaviour
{
    public TextMeshProUGUI candy;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI gamesPlayed;
    public TextMeshProUGUI score;
    public PlayerData playerData;

    public CanvasGroup textGroup;
    public CanvasGroup buttonGroup;

    public EventSO gameStartEvent;
    public EventSO scoreChangeEvent;
    public EventSO birdDieEvent;

    private void OnEnable()
    {
        gameStartEvent.ThingHappened += HideMenu;
        gameStartEvent.ThingHappened += ShowScore;
        scoreChangeEvent.ThingHappened += ChangeScoreText;
        birdDieEvent.ThingHappened += HideScore;
        birdDieEvent.ThingHappened += ShowText;
    }

    private void OnDisable()
    {
        gameStartEvent.ThingHappened -= HideMenu;
        gameStartEvent.ThingHappened += ShowScore;
        scoreChangeEvent.ThingHappened -= ChangeScoreText;
        birdDieEvent.ThingHappened -= HideScore;
        birdDieEvent.ThingHappened -= ShowText;
    }

    void Start()
    {
        playerData = GameManager.Instance.playerData;
        
        if (playerData.gamesPlayed != 0) 
        {
            candy.text = $"{playerData.candy}";
            bestScore.text = $"BEST SCORE: {playerData.bestScore}";
            gamesPlayed.text = $"GAMES PLAYED: {playerData.gamesPlayed}";
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

    void ShowText()
    {
        textGroup.DOFade(1, 1).SetUpdate(true);
    }

    void ChangeScoreText()
    {
        score.text = $"{GameManager.Instance.score}";
    }
}
