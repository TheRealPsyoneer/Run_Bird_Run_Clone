using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TutorialTextMenuUI : MonoBehaviour
{
    public static TutorialTextMenuUI Instance;

    public TextMeshProUGUI candy;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI gamesPlayed;
    public TextMeshProUGUI score;

    public CanvasGroup textGroup;

    public EventSO gameStartEvent;
    public EventSO scoreChangeEvent;
    public EventSO birdDieEvent;

    [SerializeField] List<Color> textColors;

    public CanvasGroup textGroup2;
    public TextMeshProUGUI instructionText;

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
        score.color = textColors[GameManager.Instance.playerData.themeID];
        candy.color = textColors[GameManager.Instance.playerData.themeID];
        bestScore.color = textColors[GameManager.Instance.playerData.themeID];
        gamesPlayed.color = textColors[GameManager.Instance.playerData.themeID];
        instructionText.color = textColors[GameManager.Instance.playerData.themeID];

        if (GameManager.Instance.playerData.gamesPlayed != 0)
        {
            candy.text = $"{GameManager.Instance.playerData.candy}";
            bestScore.text = $"BEST SCORE: {GameManager.Instance.playerData.bestScore}";
            gamesPlayed.text = $"GAMES PLAYED: {GameManager.Instance.playerData.gamesPlayed}";
        }
        else
        {
            candy.gameObject.SetActive(false);
            bestScore.gameObject.SetActive(false);
            gamesPlayed.gameObject.SetActive(false);
        }
    }

    void HideMenu()
    {
        textGroup2.DOFade(0, 1);
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
