using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] float showInterval;
    [SerializeField] float liveTime;
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
        lineSprite.DOColor(new Color(1, 1, 1, 0), liveTime).SetEase(Ease.InExpo);

        sprite.color = Color.white;
        sprite.DOColor(new Color(1, 1, 1, 0), liveTime).SetEase(Ease.InExpo);

        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        sprite.enabled = true;
        yield return new WaitForSeconds(showInterval);
        sprite.enabled = false;
        yield return new WaitForSeconds(showInterval);
        sprite.enabled = true;
        yield return new WaitForSeconds(showInterval);
        sprite.enabled = false;
        yield return new WaitForSeconds(showInterval); 
        sprite.enabled = true;
        yield return new WaitForSeconds(showInterval);
        gameObject.SetActive(false);
    }
}
