using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBirdDailyPrize : DailyPrize
{
    CanvasGroup canvasGroup;

    [SerializeField] Image backLight;
    [SerializeField] CanvasGroup birdPrize;
    [SerializeField] CanvasGroup takeButton;

    public void ReturnToMainMenu()
    {
        GameManager.Instance.GoToScene("Main");
    }

    public override void GivePlayerReward()
    {
        List<int> IDs = new(GameManager.Instance.cachedSpecialBirdsID);
        int randomSpecialBirdIndex = IDs[Random.Range(0, IDs.Count)];

        birdPrize.gameObject.GetComponent<Image>().sprite = GameManager.Instance.spriteLists[randomSpecialBirdIndex].sprites[0];

        GameManager.Instance.playerData.unlockedBirds[randomSpecialBirdIndex] = true;
        GameManager.Instance.specialBirdsID.Remove(randomSpecialBirdIndex);
        GameManager.Instance.playerData.SaveData();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(backLight.rectTransform.DOScale(0, 0.5f).From());
        sequence.Append(birdPrize.DOFade(1, 0.5f));
        sequence.Append(takeButton.DOFade(1, 0.5f));
        sequence.Join(takeButton.gameObject.GetComponent<Image>().rectTransform.DOLocalMoveY(-355, 0.5f));
    }
}
