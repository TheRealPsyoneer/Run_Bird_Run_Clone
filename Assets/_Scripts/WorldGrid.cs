using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid Instance;
    Grid grid;
    public int boundCellX;
    public float CelValue { get { return grid.cellSize.x; } }

    private void Awake()
    {
        Instance = this;
        grid = GetComponent<Grid>();
    }

    public Vector2Int GetWorldToCellPosition(Vector3 position)
    {
        return (Vector2Int) grid.WorldToCell(position);
    }

    public Vector3 GetCellToWorldPosition(Vector2Int cellPosition)
    {
        return grid.CellToWorld((Vector3Int)cellPosition);
    }
}
