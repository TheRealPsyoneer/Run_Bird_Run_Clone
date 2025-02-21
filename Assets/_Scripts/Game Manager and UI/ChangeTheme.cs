using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTheme : SkinItem
{
    CanvasGroup canvasGroup;
    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        unlockedEvent.ThingHappened += ThemeIsUnlocked;
    }

    private void OnDisable()
    {
        unlockedEvent.ThingHappened -= ThemeIsUnlocked;
    }

    private void Start()
    {
        if (CheckUnlocked())
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void ChangeThemeIndex()
    {
        GameManager.Instance.playerData.themeID = thisItemID;
        GameManager.Instance.playerData.SaveData();

        GameManager.Instance.ReturnToLastScene();
    }

    public override bool CheckUnlocked()
    {
        if (GameManager.Instance.playerData.unlockedThemes[thisItemID])
        {
            return true;
        }
        return false;
    }

    void ThemeIsUnlocked()
    {
        GameManager.Instance.playerData.unlockedThemes[thisItemID] = true;
        GameManager.Instance.playerData.SaveData();

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
