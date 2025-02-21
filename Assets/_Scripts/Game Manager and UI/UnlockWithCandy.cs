using DG.Tweening;
using TMPro;
using UnityEngine;

public class UnlockWithCandy : UnlockableItem
{
    [SerializeField] int candyPrice;
    TextMeshProUGUI candyPriceText;

    protected override void Awake()
    {
        base.Awake();
        candyPriceText = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();
        candyPriceText.text = $"{candyPrice}";
    }

    public override void UnlockAction()
    {
        if (GameManager.Instance.playerData.candy >= candyPrice)
        {
            int startCandy = GameManager.Instance.playerData.candy;
            TextMeshProUGUI textCandy = ThemeCanvas.Instance.candyNumber;
            DOTween.To(() => startCandy, x =>
            {
                startCandy = x;
                textCandy.text = $"{x}";
            }, startCandy - candyPrice, 1);

            GameManager.Instance.playerData.candy = startCandy - candyPrice;
            unlockableItem.unlockedEvent.Broadcast();

            unlockConditionCover.DOFade(0, 1);
            unlockConditionCover.blocksRaycasts = false;
        }
    }
}
