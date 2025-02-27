using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChallengeDetailUI : MonoBehaviour
{
    public static ChallengeDetailUI Instance;
    CanvasGroup canvasGroup;
    public TextMeshProUGUI challengeNumberText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI curProgressText;
    public CanvasGroup progress;
    public CanvasGroup completeTick;

    [SerializeField] List<Color> fillColors;
    public Image topFill;
    public Image progressBar;
    public int progressBarMoveLeftX;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowCompleteChallengeSequence()
    {
        topFill.color = fillColors[1];

        int completedChallenge = GameManager.Instance.playerData.challengeNumber;

        challengeNumberText.text = $"CHALLENGE {completedChallenge+1}";

        instructionText.text = string.Format(GameManager.Instance.challenges[completedChallenge].instruction, GameManager.Instance.challenges[completedChallenge].goal);

        progress.alpha = 0;
        completeTick.alpha = 1;
        canvasGroup.DOFade(1, 1).SetUpdate(true).OnComplete(() =>
        {
            GameManager.Instance.playerData.challengeNumber++;
            GameManager.Instance.playerData.curChallengeProgress = 0;
            StartCoroutine(ShowCompleteChallengeSequence2());
        });
    }

    IEnumerator ShowCompleteChallengeSequence2()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        canvasGroup.DOFade(0, 1).SetUpdate(true).OnComplete(() =>
        {
            topFill.color = fillColors[0];

            int completedChallenge = GameManager.Instance.playerData.challengeNumber;

            challengeNumberText.text = $"CHALLENGE {completedChallenge+1}";

            instructionText.text = string.Format(GameManager.Instance.challenges[completedChallenge].instruction, GameManager.Instance.challenges[completedChallenge].goal);
            completeTick.alpha = 0;
            canvasGroup.DOFade(1, 1).SetUpdate(true);
        });
        yield return new WaitForSecondsRealtime(3.5f);
        canvasGroup.DOFade(0, 1).SetUpdate(true);
        canvasGroup.blocksRaycasts = false;

        GameManager.Instance.playerData.SaveData();
        //GoogleManager.Instance.CloudSave();

        yield return new WaitForSecondsRealtime(0.75f);
        MenuUI.Instance.ShowText();
        DeadMenuUI.Instance.ShowMenu();
    }

    public void ShowUI()
    {
        canvasGroup.alpha = 1;

        topFill.color = fillColors[0];

        int completedChallenge = GameManager.Instance.playerData.challengeNumber;

        challengeNumberText.text = $"CHALLENGE {completedChallenge+1}";

        instructionText.text = string.Format(GameManager.Instance.challenges[completedChallenge].instruction, GameManager.Instance.challenges[completedChallenge].goal);

        curProgressText.text = $"{GameManager.Instance.playerData.curChallengeProgress}";

        float progressRatio = (float) GameManager.Instance.playerData.curChallengeProgress / GameManager.Instance.challenges[completedChallenge].goal;

        progressBar.rectTransform.DOLocalMoveX(progressBarMoveLeftX + Mathf.Abs(progressBarMoveLeftX) * progressRatio, 0);
    }
}
