using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PlayerData
{
    public int bestScore;
    public int gamesPlayed;
    public int candy;
    public int challengeNumber;

    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json", json);
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerdata.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/playerdata.json");
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            return null;
        }
    }
}
