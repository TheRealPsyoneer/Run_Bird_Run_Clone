using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionUI : MonoBehaviour
{
    public static TransitionUI Instance;
    public float transitionTime;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    private void Start()
    {
        canvasGroup.DOFade(0, transitionTime).OnComplete(() => 
        {
            GameManager.Instance.gameState = GameState.MainMenu;
            canvasGroup.blocksRaycasts = false; 
        });
    }

    public void TransitionOut()
    {
        GameManager.Instance.gameState = GameState.Pause;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, transitionTime).SetUpdate(true);
    }
}
