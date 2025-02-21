using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ChallengesSystem : MonoBehaviour
{
    public List<ChallengeSO> challenges;
    public TextMeshProUGUI ratioText;
    public TextMeshProUGUI challengeNumberText;
    public TextMeshProUGUI instructionText;

    public Image progressBar;
    public int progressBarMoveLeftX;

    void Start()
    {
        int completedChallenge = GameManager.Instance.playerData.challengeNumber;
        if (completedChallenge == challenges.Count)
        {
            gameObject.SetActive(false);
            return;
        }


        ratioText.text = $"{completedChallenge.ToString("D3")}/{challenges.Count.ToString("D3")}";

        challengeNumberText.text = $"CHALLENGE {completedChallenge }";

        instructionText.text = string.Format( challenges[completedChallenge].instruction, challenges[completedChallenge].goal);

        int progressRatio = GameManager.Instance.playerData.curChallengeProgress / challenges[completedChallenge + 1].goal;

        progressBar.rectTransform.DOLocalMoveX(progressBarMoveLeftX + Mathf.Abs(progressBarMoveLeftX) * progressRatio, 0);
    }
}
