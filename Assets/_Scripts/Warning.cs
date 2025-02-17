using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Warning : MonoBehaviour, IFactoryProduct
{
    public Stack<IFactoryProduct> pool { get; set; }
    [SerializeField] float showInterval;
    [SerializeField] float liveTime;
    [SerializeField] GameObject warningLine;
    SpriteRenderer sprite;
    SpriteRenderer lineSprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        lineSprite = warningLine.GetComponent<SpriteRenderer>();
        lineSprite.color = Color.white;
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        lineSprite.DOColor(new Color(1, 1, 1, 0), liveTime).SetEase(Ease.Linear);

        StartCoroutine(SelfActivate());
        while (true)
        {
            StartCoroutine(Anim());
        }
    }

    IEnumerator Anim()
    {
        sprite.enabled = true;
        yield return new WaitForSeconds(showInterval);
        sprite.enabled = false;
        yield return new WaitForSeconds(liveTime);
    }

    IEnumerator SelfActivate()
    {
        yield return new WaitForSeconds(liveTime);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        pool.Push(this);
    }
}
