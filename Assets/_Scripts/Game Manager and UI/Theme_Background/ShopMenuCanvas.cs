using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenuCanvas : MonoBehaviour
{
    public static ShopMenuCanvas Instance;
    public TextMeshProUGUI candyNumber;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        candyNumber.text = $"{GameManager.Instance.playerData.candy}";
    }

    public void ReturnToMenu()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GameManager.Instance.GoToScene("Main");
    }
}
