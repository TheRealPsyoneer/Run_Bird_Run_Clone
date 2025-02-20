using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteWithTheme : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] themes;
    [SerializeField] Color[] themesColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (themes.Length > 0)
        {
            spriteRenderer.sprite = themes[GameManager.Instance.playerData.themeIndex];
        }
        if (themesColor.Length > 0)
        {
            spriteRenderer.color = themesColor[GameManager.Instance.playerData.themeIndex];
        }
    }
}
