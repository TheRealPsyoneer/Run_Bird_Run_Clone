using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBirdDailyPrize : DailyPrize
{
    public override void GivePlayerReward()
    {
        List<int> IDs = new(GameManager.Instance.cachedSpecialBirdsID);
        int randomSpecialBirdIndex = IDs[Random.Range(0, IDs.Count)];

        GameManager.Instance.playerData.unlockedBirds[randomSpecialBirdIndex] = true;
        GameManager.Instance.playerData.SaveData();
    }
}
