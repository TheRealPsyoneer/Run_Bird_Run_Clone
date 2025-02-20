using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickColorChange : MonoBehaviour
{
    Image button;
    Color initColor;
    CanvasGroup canvasGroup;
    float initAlpha;
    [SerializeField] Color onClickColor;
    [SerializeField] float onClickAlpha;

    private void Awake()
    {
        button = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        initColor = button.color;
        if (canvasGroup != null)
        {
            initAlpha = canvasGroup.alpha;
        }
    }

    public void ChangeColor()
    {
        button.color = onClickColor;
    }

    public void NormalColor()
    {
        button.color = initColor;
    }

    public void ChangeAlpha()
    {
        canvasGroup.alpha = onClickAlpha;
    }

    public void NormalAlpha()
    {
        canvasGroup.alpha = initAlpha;
    }
}
