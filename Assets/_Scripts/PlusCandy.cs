using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusCandy : MonoBehaviour, IFactoryProduct
{
    public Stack<IFactoryProduct> pool { get; set; }

    [SerializeField] float flyUpDistance;

    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void FlyUp()
    {
        transform.DOMoveY(transform.position.y + flyUpDistance, 1);
        sprite.color = Color.white;
        sprite.DOColor(new Color(1, 1, 1, 0), 1).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
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
