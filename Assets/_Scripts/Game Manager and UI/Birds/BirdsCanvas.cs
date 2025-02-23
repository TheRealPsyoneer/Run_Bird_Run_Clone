using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BirdsCanvas : MonoBehaviour
{
    public static BirdsCanvas Instance;
    public TextMeshProUGUI candyNumber;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        candyNumber.text = $"{GameManager.Instance.playerData.candy}";
    }

    public void ReturnToLastScene()
    {
        GameManager.Instance.ReturnToLastScene();
    }
}
