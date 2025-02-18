using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    Image button;
    [SerializeField] float transparentValue;

    private void Awake()
    {
        button = GetComponent<Image>();
        button.color = new Color(1,1,1,transparentValue);
    }

    public void GlowUp()
    {
        button.color = Color.white;
    }

    public void GlowDown()
    {
        button.color = new Color(1, 1, 1, transparentValue);
    }
}
