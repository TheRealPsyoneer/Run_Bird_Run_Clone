using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCanvas : MonoBehaviour
{
    [SerializeField] float introTime;
    void Start()
    {
        TransitionUI.Instance.TransitionOut();
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(introTime);
        if (GameManager.Instance.playerData.isFirstTime)
        {
            GameManager.Instance.playerData.isFirstTime = false;
            GameManager.Instance.playerData.SaveData();
            //GoogleManager.Instance.CloudSave();
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }
}
