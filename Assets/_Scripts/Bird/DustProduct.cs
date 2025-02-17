using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DustProduct : MonoBehaviour, IFactoryProduct
{
    public Stack<IFactoryProduct> pool { get; set; }
    [SerializeField] float liveTime;
    [SerializeField] float initSize;
    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        float randomAngle = Random.Range(-180f, 180f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomAngle));
        transform.localScale = new Vector3(initSize, initSize, initSize);

        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence.Append(transform.DOScale(initSize, 0.1f));
        scaleSequence.Append(transform.DOScale(0, liveTime));
        

        Sequence sequence = DOTween.Sequence();
        sequence.Append(sprite.DOColor(Color.white, 0.1f));
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
