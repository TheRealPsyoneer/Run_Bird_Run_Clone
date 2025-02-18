using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxSpawner : MonoBehaviour
{
    public static BoxSpawner Instance;
    public int spawnPositionY { get; set; }

    [SerializeField] MyFactorySO boxFactory;
    public int maxSameTimeBoxFall;
    public Dictionary<int, int> colummBoxQuantity;

    int columnHighestQuantity, columnLowestQuantity;
    [SerializeField] int maxColumnQuantityDifference;

    int currentLowestRow;
    [SerializeField] List<int> boxDir;
    [SerializeField] EventSO boxFallCompleteEvent;
    [SerializeField] EventSO cameraPositionChangeEvent;
    public Dictionary<BoxBehaviour, int> boxColumnNumber = new();
    int curWaveColumnLowestQuantity;
    bool lastWaveDropDone;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        boxFactory.Initialize();
        spawnPositionY = WorldGrid.Instance.GetWorldToCellPosition(transform.position).y;

        lastWaveDropDone = true;
        SetUpColumn();
    }

    private void OnEnable()
    {
        boxFallCompleteEvent.ThingHappenedBox += OnBoxFallComplete;
    }

    private void OnDisable()
    {
        boxFallCompleteEvent.ThingHappenedBox -= OnBoxFallComplete;
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
        if (lastWaveDropDone)
        {
            int randomSameTimeBoxFall = Random.Range(1, maxSameTimeBoxFall + 1);
            HashSet<int> columns = new();
            lastWaveDropDone = false;

            List<int> fallColumns = GetFallColumns(columns, randomSameTimeBoxFall);
            StartCoroutine(SpawnBox(fallColumns));
        }
    }

    public List<int> GetFallColumns(HashSet<int> columns, int numbers)
    {
        List<int> fallColumns = new();
        int positionCellX = -1;
        columns.Add(-1);
        for (int j = 0; j < numbers; j++)

        {
            if (columnHighestQuantity - columnLowestQuantity == maxColumnQuantityDifference)

            {
                List<int> temp = new();
                for (int i = 0; i < WorldGrid.Instance.boundCellX; i++)
                {
                    if (colummBoxQuantity[i] == columnLowestQuantity)
                    {
                        temp.Add(i);
                    }
                }

                int count = 0;
                foreach (int column in temp)
                {
                    if (columns.Contains(column))
                    {
                        count++;
                    }
                }

                if (count == temp.Count)
                {
                    while (columns.Contains(positionCellX))
                    {
                        positionCellX = Random.Range(0, WorldGrid.Instance.boundCellX);
                    }
                }
                else
                {
                    while (columns.Contains(positionCellX))
                    {
                        positionCellX = temp[Random.Range(0, temp.Count)];
                    }
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
            fallColumns.Add(positionCellX);
        }

        int lowestQuantity = int.MaxValue;
        for (int i = 0; i < fallColumns.Count; i++)
        {
            if (colummBoxQuantity[fallColumns[i]] < lowestQuantity)
            {
                lowestQuantity = colummBoxQuantity[fallColumns[i]];
                curWaveColumnLowestQuantity = fallColumns[i];
            }
        }


        return fallColumns;
    }

    public IEnumerator SpawnBox(List<int> fallColumns)
    {
        for (int i = 0; i < fallColumns.Count; i++)
        {
            WarningSpawner.Instance.GetWarning(fallColumns[i]);
        }

        yield return new WaitForSeconds(WarningSpawner.Instance.warningTime);

        for (int i = 0; i < fallColumns.Count; i++)
        {
            BoxBehaviour box = (BoxBehaviour)boxFactory.GetProduct();
            box.transform.position = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(fallColumns[i], spawnPositionY));

            Vector3 dir = box.transform.localScale;
            dir.x *= boxDir[Random.Range(0, boxDir.Count)];
            box.transform.localScale = dir;

            box.targetFallCell = new Vector2Int(fallColumns[i], colummBoxQuantity[fallColumns[i]]);
            boxColumnNumber[box] = fallColumns[i];

            box.isInColumnLowestQuantity = fallColumns[i] == curWaveColumnLowestQuantity ? true : false;

            box.Falling();
        }
    }

    public void OnBoxFallComplete(BoxBehaviour box)
    {
        if (box.isInColumnLowestQuantity)
        {
            lastWaveDropDone = true;
        }

        colummBoxQuantity[boxColumnNumber[box]]++;

        int count = 0;
        for (int i = 0; i < WorldGrid.Instance.boundCellX; i++)
        {
            if (colummBoxQuantity[i] > columnLowestQuantity)
            {
                count++;
            }

            if (count == WorldGrid.Instance.boundCellX)
            {

                currentLowestRow++;
                columnLowestQuantity++;
                spawnPositionY++;
                cameraPositionChangeEvent.Broadcast();
            }

            if (colummBoxQuantity[boxColumnNumber[box]] > columnHighestQuantity)
            {
                columnHighestQuantity++;
            }
        }
    }
}
