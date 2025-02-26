using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MenuButtonCanvas : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public EventSO gameStartEvent;
    [SerializeField] List<Sprite> volumeButtonSprites;
    [SerializeField] Image volumeButton;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (GameManager.Instance.playerData.soundMuted)
        {
            volumeButton.sprite = volumeButtonSprites[1];
        }
        else
        {
            volumeButton.sprite = volumeButtonSprites[0];
        }
    }

    private void OnEnable()
    {
        gameStartEvent.ThingHappened += HideMenu;
    }

    private void OnDisable()
    {
        gameStartEvent.ThingHappened -= HideMenu;
    }

    public void LoadThemeScene()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GameManager.Instance.GoToScene("Theme");
    }

    public void LoadChallengesScene()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GameManager.Instance.GoToScene("Challenges");
    }

    public void LoadBirdsScene()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GameManager.Instance.GoToScene("Birds");
    }

    public void LoadDailyScene()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GameManager.Instance.GoToScene("Daily");
    }

    public void LoadTutorialScene()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GameManager.Instance.GoToScene("Tutorial");
    }

    public void ShowAchievement()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GoogleManager.Instance.ShowAchievement();
    }

    public void ShowLeaderboard()
    {
        AudioManager.Instance.PlayAudioClip("Button");
        GoogleManager.Instance.ShowLeaderboard();
    }

    public void TurnOnOffSound()
    {
        GameManager.Instance.playerData.soundMuted = !GameManager.Instance.playerData.soundMuted;
        if (GameManager.Instance.playerData.soundMuted)
        {
            volumeButton.sprite = volumeButtonSprites[1];
            AudioManager.Instance.audioMixer.SetFloat("Master", -80f);
        }
        else
        {
            volumeButton.sprite = volumeButtonSprites[0];
            AudioManager.Instance.audioMixer.SetFloat("Master", 1f);
        }
        GameManager.Instance.playerData.SaveData();
    }

    void HideMenu()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
    }
}
