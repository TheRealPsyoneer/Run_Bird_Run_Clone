using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickColorChange : MonoBehaviour
{
    Image button;
    Color initColor;
    [SerializeField] Color OnClickColor;

    private void Awake()
    {
        button = GetComponent<Image>();
        initColor = button.color;
    }

    public void ChangeColor()
    {
        button.color = OnClickColor;
    }

    public void NormalColor()
    {
        button.color = initColor;
    }
}
