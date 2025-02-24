using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDailyPrize : DailyPrize
{
    [SerializeField] int candyAmount;
    [SerializeField] CanvasGroup canvasGroup;

    public override void GivePlayerReward()
    {
        GameManager.Instance.playerData.candy += candyAmount;
        GameManager.Instance.playerData.SaveData();
    }
}
