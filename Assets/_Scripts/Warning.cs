using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public static float LiveTime;
    [SerializeField] GameObject warningLine;
    SpriteRenderer sprite;
    SpriteRenderer lineSprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        lineSprite = warningLine.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        lineSprite.color = Color.white;
        lineSprite.DOColor(new Color(1, 1, 1, 0), LiveTime).SetEase(Ease.InExpo);

        sprite.color = Color.white;
        sprite.DOColor(new Color(1, 1, 1, 0), LiveTime).SetEase(Ease.InExpo);

        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        sprite.enabled = true;
        yield return new WaitForSeconds(LiveTime * 0.2f);
        sprite.enabled = false;
        yield return new WaitForSeconds(LiveTime * 0.2f);
        sprite.enabled = true;
        yield return new WaitForSeconds(LiveTime * 0.2f);
        sprite.enabled = false;
        yield return new WaitForSeconds(LiveTime * 0.2f); 
        sprite.enabled = true;
        yield return new WaitForSeconds(LiveTime * 0.2f);
        gameObject.SetActive(false);
    }
}
