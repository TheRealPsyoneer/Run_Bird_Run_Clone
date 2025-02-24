using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeTheme : SkinItem
{
    [SerializeField] CanvasGroup itemImage;

    private void OnEnable()
    {
        unlockedEvent.ThingHappened += ThemeUnlockAction;
    }

    private void OnDisable()
    {
        unlockedEvent.ThingHappened -= ThemeUnlockAction;
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

    public void ChangeThemeIndex()
    {
        GameManager.Instance.playerData.themeID = thisItemID;
        GameManager.Instance.playerData.SaveData();

        GameManager.Instance.GoToScene("Main");
    }

    public override bool CheckUnlocked()
    {
        if (GameManager.Instance.playerData.unlockedThemes[thisItemID])
        {
            return true;
        }
        return false;
    }

    void ThemeUnlockAction()
    {
        GameManager.Instance.playerData.unlockedThemes[thisItemID] = true;
        itemImage.gameObject.SetActive(true);
        GameManager.Instance.playerData.SaveData();
    }
}
