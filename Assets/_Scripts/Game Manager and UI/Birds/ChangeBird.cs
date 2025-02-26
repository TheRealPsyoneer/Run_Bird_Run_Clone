using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBird : SkinItem
{
    [SerializeField] CanvasGroup itemImage;

    private void OnEnable()
    {
        unlockedEvent.ThingHappened += BirdUnlockAction;
    }

    private void OnDisable()
    {
        unlockedEvent.ThingHappened -= BirdUnlockAction;
    }

    private void Start()
    {
        if (CheckUnlocked())
        {
            itemImage.gameObject.SetActive(true);
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
    }

    public void ChangeBirdIndex()
    {
        AudioManager.Instance.PlayAudioClip("ItemClick");
        GameManager.Instance.playerData.birdID = thisItemID;
        GameManager.Instance.playerData.SaveData();
        //GoogleManager.Instance.CloudSave();

        GameManager.Instance.GoToScene("Main");
    }

    public override bool CheckUnlocked()
    {
        if (GameManager.Instance.playerData.unlockedBirds[thisItemID])
        {
            return true;
        }
        return false;
    }

    void BirdUnlockAction()
    {
        GameManager.Instance.playerData.unlockedBirds[thisItemID] = true;
        GameManager.Instance.playerData.unlockedBirdsNumber++;
        GameManager.Instance.CheckAchievement();

        itemImage.gameObject.SetActive(true);
        GameManager.Instance.playerData.SaveData();
        //GoogleManager.Instance.CloudSave();
    }
}
