using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using System;

public class GoogleManager : MonoBehaviour
{
    public static GoogleManager Instance;
    //public const string saveFileName = "player_save";

    private void Awake()
    {
        Instance = this;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI75mzus0REAIQAg");
        }
        else
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
        }
    }

    public void ShowAchievement()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
        }
    }

    //public void CloudSave()
    //{
    //    if (!Social.localUser.authenticated) return;

    //    PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(
    //        saveFileName,
    //        DataSource.ReadCacheOrNetwork,
    //        ConflictResolutionStrategy.UseLongestPlaytime,
    //        (status, meta) =>
    //        {
    //            if (status == SavedGameRequestStatus.Success)
    //            {
    //                string json = JsonUtility.ToJson(GameManager.Instance.playerData);
    //                byte[] data = Encoding.UTF8.GetBytes(json);
    //                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder()
    //                    .WithUpdatedDescription("Game save at " + DateTime.Now)
    //                    .Build();

    //                PlayGamesPlatform.Instance.SavedGame.CommitUpdate(meta, update, data, (saveStatus, savedMeta) =>
    //                {
    //                    Debug.Log(saveStatus == SavedGameRequestStatus.Success ? "Game saved!" : "Save failed.");
    //                });
    //            }
    //            else
    //            {
    //                Debug.Log("Failed to open save file.");
    //            }
    //        });
    //}

    //public void CloudLoad()
    //{
    //    if (!Social.localUser.authenticated) return;

    //    PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(
    //        saveFileName,
    //        DataSource.ReadCacheOrNetwork,
    //        ConflictResolutionStrategy.UseLongestPlaytime,
    //        (status, meta) =>
    //        {
    //            if (status == SavedGameRequestStatus.Success)
    //            {
    //                PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(meta, (readStatus, data) =>
    //                {
    //                    if (readStatus == SavedGameRequestStatus.Success && data.Length > 0)
    //                    {
    //                        string json = Encoding.UTF8.GetString(data);
    //                        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

    //                        GameManager.Instance.playerData = loadedData;
    //                    }
    //                    else
    //                    {
    //                        GameManager.Instance.playerData = new();
    //                        GameManager.Instance.playerData.unlockedBirds = new();
    //                        GameManager.Instance.playerData.unlockedThemes = new();
    //                        GameManager.Instance.playerData.isFirstTime = true;
    //                    }
    //                });
    //            }
    //            else
    //            {
                    
    //            }
    //        });
    //}
}
