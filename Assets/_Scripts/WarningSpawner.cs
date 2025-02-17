using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSpawner : MonoBehaviour
{
    public static WarningSpawner Instance;
    public  List<GameObject> warningList;
    public float warningTime;

    private void Awake()
    {
        Instance = this;
    }

    public void GetWarning(int columnNumber)
    {
        warningList[columnNumber].gameObject.SetActive(true);
    }
}
