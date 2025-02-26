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
        AudioManager.Instance.PlayAudioClip("DailyPrize");

        GameManager.Instance.playerData.candy += candyAmount;
        GameManager.Instance.playerData.SaveData();
        //GoogleManager.Instance.CloudSave();

        candyAmountText.text = $"+{candyAmount}";

        backLight.rectTransform.DOLocalRotate(new Vector3(0, 0, 360), 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(backLight.rectTransform.DOScale(0, 0.5f).From());
        sequence.Append(candyPrize.DOFade(1, 0.5f));
        sequence.Append(takeButton.DOFade(1, 0.5f));
        sequence.Join(takeButton.gameObject.GetComponent<Image>().rectTransform.DOLocalMoveY(-355, 0.5f));
        
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GameManager.Instance.GoToScene("Main");
    }
}
