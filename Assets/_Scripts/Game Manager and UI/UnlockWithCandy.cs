using DG.Tweening;
using TMPro;
using UnityEngine;

public class UnlockWithCandy : UnlockableItem
{
    int candyPrice;
    TextMeshProUGUI candyPriceText;

    protected override void Awake()
    {
        base.Awake();
        candyPriceText = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();
        candyPrice = int.Parse(candyPriceText.text);
    }

    public override void UnlockAction()
    {
        if (GameManager.Instance.playerData.candy >= candyPrice)
        {
            AudioManager.Instance.PlayAudioClip("ItemUnlock");

            unlockConditionCover.interactable = false;

            int startCandy = GameManager.Instance.playerData.candy;
            TextMeshProUGUI textCandy = ShopMenuCanvas.Instance.candyNumber;
            DOTween.To(() => startCandy, x =>
            {
                startCandy = x;
                textCandy.text = $"{x}";
            }, startCandy - candyPrice, 1);

            GameManager.Instance.playerData.candy = startCandy - candyPrice;
            unlockableItem.unlockedEvent.Broadcast();

            unlockConditionCover.DOFade(0, 1).OnComplete(() => unlockConditionCover.gameObject.SetActive(false));
        }
        else
        {
            AudioManager.Instance.PlayAudioClip("Error");
        }
    }
}
