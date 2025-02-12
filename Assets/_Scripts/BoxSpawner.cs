using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public int spawnPositionY {  get; set; }
    public float spawnInterval;

    [SerializeField] MyFactorySO boxFactory;
    public int maxSameTimeBoxFall;
    Dictionary<int, int> colummBoxQuantity;

    int columnHighestQuantity, columnLowestQuantity;
    [SerializeField] int maxColumnQuantityDifference;

    float lastSpawnTime;
    int currentLowestRow;
    [SerializeField] List<int> boxDir;

    void Start()
    {
        lastSpawnTime = Time.time;
        boxFactory.Initialize();
        spawnPositionY = WorldGrid.Instance.GetWorldToCellPosition(transform.position).y;

        SetUpColumn();
    }

    private void SetUpColumn()
    {
        colummBoxQuantity = new();
        for (int i = 0; i < WorldGrid.Instance.boundCellX; i++)
        {
            colummBoxQuantity.Add(i, 0);
        }
        columnHighestQuantity = 0;
        columnLowestQuantity = 0;
        currentLowestRow = 0;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnInterval)
        {
            int randomSameTimeBoxFall = Random.Range(1, maxSameTimeBoxFall+1);
            HashSet<int> columns = new();
            columns.Add(-1);
            for (int i = 0; i<randomSameTimeBoxFall; i++)
            {
                SpawnBox(columns);
            }
            
            lastSpawnTime = Time.time;
        }
    }

    public int SpawnBox(HashSet<int> columns)
    {
        int positionCellX = -1;

        
        if (columnHighestQuantity - columnLowestQuantity == maxColumnQuantityDifference)
        {
            List<int> temp = new();
            for (int i=0; i < WorldGrid.Instance.boundCellX; i++)
            {
                if (colummBoxQuantity[i] == columnLowestQuantity)
                {
                    temp.Add(i);
                }
            }

            while (columns.Contains(positionCellX))
            {
                positionCellX = temp[Random.Range(0, temp.Count)];
            }
        }
        else
        {
            while (columns.Contains(positionCellX))
            {
                positionCellX = Random.Range(0, WorldGrid.Instance.boundCellX);
            }
        }
        columns.Add(positionCellX);

        BoxBehaviour box = (BoxBehaviour) boxFactory.GetProduct();
        box.transform.position = WorldGrid.Instance.GetCellToWorldPosition( new Vector2Int(positionCellX, spawnPositionY));
        Vector3 dir = box.transform.localScale;
        dir.x *= boxDir[Random.Range(0, boxDir.Count)];
        box.transform.localScale = dir;
        box.targetFallCell = new Vector2Int(positionCellX, colummBoxQuantity[positionCellX]);
        colummBoxQuantity[positionCellX]++;

        int count = 0;
        for (int i=0; i < WorldGrid.Instance.boundCellX; i++)
        {
            if (colummBoxQuantity[i] > columnLowestQuantity)
            {
                count++;
            }
        }
        if (count == WorldGrid.Instance.boundCellX)
        {
            currentLowestRow++;
            columnLowestQuantity++;
            spawnPositionY++;
            transform.DOMoveY(spawnPositionY, 0);
        }

        if (colummBoxQuantity[positionCellX] > columnHighestQuantity)
        {
            columnHighestQuantity++;
        }

        box.Falling();

        return positionCellX;

        
    }
}
