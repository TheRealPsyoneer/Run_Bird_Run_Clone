using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BoxBehaviour : MonoBehaviour, IFactoryProduct
{
    
    public static float FallTimePerCell;
    public Stack<IFactoryProduct> pool { get; set; }
    public Vector2Int targetFallCell { get; set; }
    [SerializeField] EventSO boxFallCompleteEvent;
    public bool isClimbable;
    AudioSource impactSound;

    private void Awake()
    {
        impactSound = GetComponent<AudioSource>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        isClimbable = false;
    }

    public void Falling()
    {
        Vector2Int startCellPosition = WorldGrid.Instance.GetWorldToCellPosition(transform.position);
        Vector3 targetPosition = WorldGrid.Instance.GetCellToWorldPosition(targetFallCell);
        transform.DOKill();
        transform.DOMove(targetPosition, (Mathf.Abs(startCellPosition.y - targetFallCell.y)) * FallTimePerCell)
            .SetEase(Ease.Linear)
            .OnComplete(WhenComplete);
    }

    void WhenComplete()
    {
        impactSound.Play();
        boxFallCompleteEvent.Broadcast(this);
        boxFallCompleteEvent.Broadcast();
        isClimbable = true;
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
