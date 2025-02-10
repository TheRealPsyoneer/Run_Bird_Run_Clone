using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BoxBehaviour : MonoBehaviour, IFactoryProduct
{
    [SerializeField] float fallTimePerCell;
    public Stack<IFactoryProduct> pool { get; set; }
    public Vector2Int targetFallCell { get; set; }
    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void Falling()
    {
        Vector2Int startCellPosition = WorldGrid.Instance.GetWorldToCellPosition(transform.position);
        Vector3 targetPosition = WorldGrid.Instance.GetCellToWorldPosition(targetFallCell);
        transform.DOKill();
        transform.DOMove(targetPosition, (Mathf.Abs(startCellPosition.y-targetFallCell.y))*fallTimePerCell).SetEase(Ease.Linear);
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
