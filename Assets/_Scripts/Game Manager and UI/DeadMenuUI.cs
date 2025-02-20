using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeadMenuUI : MonoBehaviour
{
    public TextMeshProUGUI points;

    [HideInInspector]
    public CanvasGroup[] canvasGroups;

    bool interactable;

    private void Awake()
    {
        canvasGroups = GetComponentsInChildren<CanvasGroup>();
    }

    public void ShowMenu()
    {
        points.text = $"{GameManager.Instance.score}";
        interactable = false;
        gameObject.SetActive(true);
        StartCoroutine(StartShowing());
    }

    IEnumerator StartShowing()
    {
        yield return new WaitForSecondsRealtime(1);
        canvasGroups[0].DOFade(1, 0.2f).SetUpdate(true);
        canvasGroups[0].interactable = true;
        yield return new WaitForSecondsRealtime(0.2f);
        canvasGroups[1].DOFade(1, 0.2f).SetUpdate(true);
        canvasGroups[1].interactable = true;
        yield return new WaitForSecondsRealtime(0.2f);
        canvasGroups[2].DOFade(1, 0.2f).SetUpdate(true);
        canvasGroups[2].interactable = true;
        interactable = true;
    }

    public void Replay()
    {
        if (!interactable) return;

        GameManager.Instance.GameRestart();
    }

    public void ButtonDownColorChange()
    {

    }
}
