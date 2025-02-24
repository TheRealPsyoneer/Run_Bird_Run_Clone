using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ChallengesCanvas : MonoBehaviour
{
    public TextMeshProUGUI ratioText;

    void Start()
    {
        int completedChallenge = GameManager.Instance.playerData.challengeNumber;

        ratioText.text = $"{completedChallenge.ToString("D3")}/{GameManager.Instance.challenges.Count.ToString("D3")}";

        if (completedChallenge <= GameManager.Instance.challenges.Count)
        {
            ChallengeDetailUI.Instance.ShowUI();
        }
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.GoToScene("Main");
    }
}
