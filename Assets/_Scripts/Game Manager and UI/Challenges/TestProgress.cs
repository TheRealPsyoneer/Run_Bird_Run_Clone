using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProgress : MonoBehaviour
{

    int progress;
    private void Start()
    {
        progress = GameManager.Instance.playerData.curChallengeProgress;
    }
    void Update()
    {
        if (progress != GameManager.Instance.playerData.curChallengeProgress)
        {
            progress = GameManager.Instance.playerData.curChallengeProgress;
            Debug.Log(progress);
        }
    }
}
