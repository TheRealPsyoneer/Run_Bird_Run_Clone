using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour, IFactoryProduct
{
    public Stack<IFactoryProduct> pool { get; set; }
    public Vector2Int targetFallCell { get; set; }

    public int value;

    SpriteRenderer sprite;
    Collider2D col;

    [SerializeField] MyFactorySO plusCandyFactory;
    bool factoryInitialized;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        factoryInitialized = false;
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        if (!factoryInitialized)
        {
            plusCandyFactory.Initialize();
            factoryInitialized = true;
        }
    }

    public void Falling()
    {
        Vector2Int startCellPosition = WorldGrid.Instance.GetWorldToCellPosition(transform.position);
        Vector3 targetPosition = WorldGrid.Instance.GetCellToWorldPosition(targetFallCell);
        targetPosition.y -= 0.1f;
        transform.DOKill();
        transform.DOMove(targetPosition, (Mathf.Abs(startCellPosition.y - targetFallCell.y)) * BoxBehaviour.FallTimePerCell)
            .SetEase(Ease.Linear)
            .OnComplete(WhenComplete);
    }

    void WhenComplete()
    {
        transform.DOMoveY(transform.position.y + 0.2f, 1f).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        pool.Push(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            AudioManager.Instance.PlayAudioClip("Candy");

            col.enabled = false;

            GameManager.Instance.playerData.candy += value;
            if (GameManager.Instance.challenges[GameManager.Instance.playerData.challengeNumber].challengeType == ChallengeType.CollectCandiesSingleGame
                || GameManager.Instance.challenges[GameManager.Instance.playerData.challengeNumber].challengeType == ChallengeType.CollectTotalCandies)
            {
                GameManager.Instance.playerData.curChallengeProgress += value;
            }

            PlusCandy instance = (PlusCandy) plusCandyFactory.GetProduct();
            instance.transform.position = transform.position + new Vector3(0,WorldGrid.Instance.CellValue * 0.5f);
            instance.FlyUp();

            transform.DOKill();
            sprite.DOColor(new Color(1, 1, 1, 0), 0.5f)
                .OnComplete(() => gameObject.SetActive(false));
        }

    }
}
