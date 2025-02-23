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
        GameManager.Instance.GoToScene("Theme");
    }

    public void LoadChallengesScene()
    {
        GameManager.Instance.GoToScene("Challenges");
    }

    public void LoadBirdsScene()
    {
        GameManager.Instance.GoToScene("Birds");
    }

    void HideMenu()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
    }
}
