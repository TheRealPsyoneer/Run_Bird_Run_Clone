using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThemeCanvas : MonoBehaviour
{
    public static ThemeCanvas Instance;
    public TextMeshProUGUI candyNumber;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        candyNumber.text = $"{GameManager.Instance.playerData.candy}";
    }
}
