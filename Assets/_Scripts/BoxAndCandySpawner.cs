using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxAndCandySpawner : MonoBehaviour
{
    public static BoxAndCandySpawner Instance;
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
    public float WaveInterval;

    [Header("Candy")]
    [SerializeField] MyFactorySO[] candyFactory;
    public static MyFactorySO[] CandyFactory;
    public static int candyNumber;
    [SerializeField] float candyDropChance;
    bool candyDropppedThisWave;

    private void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        boxFactory.Initialize();
        foreach (MyFactorySO factory in candyFactory)
        {
            factory.Initialize();
        }
        CandyFactory = candyFactory;

        spawnPositionY = WorldGrid.Instance.GetWorldToCellPosition(transform.position).y;

        lastWaveDropDone = true;
        SetUpColumn();
        SpawnRandomBoxes();

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

    void SpawnRandomBoxes()
    {
        for (int i = 0; i < colummBoxQuantity.Count; i++)
        {
            int randomBoxes = Random.Range(0, 3);
            for (int j = 0; j < randomBoxes; j++)
            {
                BoxBehaviour box = (BoxBehaviour)boxFactory.GetProduct();
                box.isClimbable = true;
                box.transform.position = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(i, j));
                colummBoxQuantity[i]++;
            }
        }
    }

    public void InitialCheck()
    {
        int time = 1;
        while (time <= 2)
        {
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

                if (colummBoxQuantity[i] > columnHighestQuantity)
                {
                    columnHighestQuantity = colummBoxQuantity[i];
                }
            }
            time++;
        }
    }

    void Update()
    {
        if (lastWaveDropDone && GameManager.Instance.gameState == GameState.Playing)
        {
            int randomSameTimeBoxFall = Random.Range(1, maxSameTimeBoxFall + 1);
            HashSet<int> columns = new();
            lastWaveDropDone = false;

            List<int> fallColumns = GetFallColumns(columns, randomSameTimeBoxFall);
            StartCoroutine(SpawnBox(fallColumns));
            StartCoroutine(NextWave());

            SpawnCandy();
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(WaveInterval);
        lastWaveDropDone = true;
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

        yield return new WaitForSeconds(Warning.LiveTime);

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

            colummBoxQuantity[boxColumnNumber[box]]++;

            if (colummBoxQuantity[boxColumnNumber[box]] > columnHighestQuantity)
            {
                columnHighestQuantity++;
            }

            
        }

        //int count = 0;
        //for (int j = 0; j < WorldGrid.Instance.boundCellX; j++)
        //{
        //    if (colummBoxQuantity[j] > columnLowestQuantity)
        //    {
        //        count++;
        //    }
        //}
        //if (count == WorldGrid.Instance.boundCellX)
        //{
            
        //}
    }

    public void OnBoxFallComplete(BoxBehaviour box)
    {
        int count = 0;
        for (int i = 0; i < WorldGrid.Instance.boundCellX; i++)
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
            cameraPositionChangeEvent.Broadcast();
        }
    }

    void SpawnCandy()
    {
        float randomChance = Random.Range(0, 100f);
        if (randomChance < candyDropChance)
        {
            int randomColumn = Random.Range(0,WorldGrid.Instance.boundCellX);
            int randomCandy = Random.Range(0, candyNumber + 1);

            Candy candy = (Candy)candyFactory[randomCandy].GetProduct();

            candy.transform.position = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(randomColumn, spawnPositionY));

            candy.targetFallCell = new Vector2Int(randomColumn, colummBoxQuantity[randomColumn]);

            candy.Falling();
        }
    }
}
