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
    public int curChallengeProgress;
    public int challengeNumber;
    public bool isFirstTime;
    public string lastDailyPrizeDate;
    public bool soundMuted;

    public int birdID;
    public List<bool> unlockedBirds;
    public int unlockedBirdsNumber;

    public int themeID;
    public List<bool> unlockedThemes;

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
