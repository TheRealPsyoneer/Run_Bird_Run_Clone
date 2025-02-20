using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTheme : MonoBehaviour
{
    [SerializeField] int themeIndex;

    public void ChangeThemeIndex()
    {
        GameManager.Instance.playerData.themeIndex = themeIndex;
        GameManager.Instance.playerData.SaveData();

        GameManager.Instance.ReturnToLastScene();
    }
}
