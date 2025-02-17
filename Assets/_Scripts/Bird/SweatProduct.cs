using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SweatProduct : MonoBehaviour, IFactoryProduct
{
    public Stack<IFactoryProduct> pool { get; set; }
    public float liveTime;
    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(sprite.DOColor(Color.white, 0.2f));
        sequence.Append(sprite.DOColor(new Color(1, 1, 1, 0), liveTime).OnComplete(() => gameObject.SetActive(false)));
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
