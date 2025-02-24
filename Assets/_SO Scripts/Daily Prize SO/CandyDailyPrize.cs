using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CandyDailyPrize : DailyPrize
{
    [SerializeField] int candyAmount;
    CanvasGroup canvasGroup;

    [SerializeField] Image backLight;
    [SerializeField] CanvasGroup candyPrize;
    [SerializeField] CanvasGroup takeButton;
    [SerializeField] TextMeshProUGUI candyAmountText;

    public override void GivePlayerReward()
    {
        GameManager.Instance.playerData.candy += candyAmount;
        GameManager.Instance.playerData.SaveData();

        candyAmountText.text = $"+{candyAmount}";

        Sequence sequence = DOTween.Sequence();
        sequence.Append(backLight.rectTransform.DOScale(0, 0.5f).From());
        sequence.Append(candyPrize.DOFade(1, 0.5f));
        sequence.Append(takeButton.DOFade(1, 0.5f));
        sequence.Join(takeButton.gameObject.GetComponent<Image>().rectTransform.DOLocalMoveY(-355, 0.5f));
        
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.GoToScene("Main");
    }
}
