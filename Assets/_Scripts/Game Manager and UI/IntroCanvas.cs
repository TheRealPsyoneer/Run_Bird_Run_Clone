using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCanvas : MonoBehaviour
{
    [SerializeField] float introTime;
    void Start()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(introTime);
        if (GameManager.Instance.playerData.isFirstTime)
        {
            GameManager.Instance.playerData.isFirstTime = false;
            GameManager.Instance.playerData.SaveData();
            GameManager.Instance.GoToScene("Tutorial");
        }
        else
        {
            GameManager.Instance.GoToScene("Main");
        }
    }
}
