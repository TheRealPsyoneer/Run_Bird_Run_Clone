using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuButtonCanvas : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public EventSO gameStartEvent;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        gameStartEvent.ThingHappened += HideMenu;
    }

    private void OnDisable()
    {
        gameStartEvent.ThingHappened -= HideMenu;
    }

    public void LoadThemeScene()
    {
        TransitionUI.Instance.TransitionOut();

        StartCoroutine(LoadingScene("Theme"));
    }

    IEnumerator LoadingScene(string sceneName)
    {
        yield return new WaitForSecondsRealtime(TransitionUI.Instance.transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    void HideMenu()
    {
        canvasGroup.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
    }
}
