using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveButton : MonoBehaviour
{
    Image button;
    CanvasGroup canvas;
    [SerializeField] float transparentValue;
    public EventSO birdDieEvent;
    

    private void Awake()
    {
        button = GetComponent<Image>();
        canvas = GetComponent<CanvasGroup>();
        button.color = new Color(1,1,1,transparentValue);
    }

    private void OnEnable()
    {
        birdDieEvent.ThingHappened += TurnOff;
    }

    private void OnDisable()
    {
        birdDieEvent.ThingHappened -= TurnOff;
    }

    public void GlowUp()
    {
        button.color = Color.white;
    }

    public void GlowDown()
    {
        button.color = new Color(1, 1, 1, transparentValue);
    }

    void TurnOff()
    {
        canvas.DOFade(0,1).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
    }
}
