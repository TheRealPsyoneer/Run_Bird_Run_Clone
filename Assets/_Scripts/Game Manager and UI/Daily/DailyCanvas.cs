using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class DailyCanvas : MonoBehaviour
{
    [SerializeField] Button wheel1;
    [SerializeField] Button wheel2;
    public Button curWheel;

    [SerializeField] TextMeshProUGUI checkingConnectionText;
    public DateTime internetTime;
    public EventSO gotInternetTime;

    bool isSpinning;
    List<DailyPrize> prizeList;

    private void Awake()
    {
        gotInternetTime = ScriptableObject.CreateInstance<EventSO>();
    }

    private void OnEnable()
    {
        gotInternetTime.ThingHappened += CheckingPlayerDaily;
    }

    private void OnDisable()
    {
        gotInternetTime.ThingHappened -= CheckingPlayerDaily;
    }

    private void Start()
    {
        if (GameManager.Instance.cachedSpecialBirdsID.Count > 0)
        {
            curWheel = wheel1;
            curWheel.gameObject.SetActive(true);
            wheel2.gameObject.SetActive(false);
        }
        else
        {
            curWheel = wheel2;
            curWheel.gameObject.SetActive(true);
            wheel1.gameObject.SetActive(false);
        }

        curWheel.interactable = false;

        StartCoroutine(GetInternetTime());
    }

    IEnumerator GetInternetTime()
    {
        string[] timeURLs = new string[]
        {
            "http://worldclockapi.com/api/json/utc/now",
            "http://worldtimeapi.org/api/timezone/Etc/UTC",
            "http://time.google.com"
        };

        foreach (string url in timeURLs)
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
            unityWebRequest.timeout = 5;

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                if (url.Contains("google.com"))
                {
                    string dateStr = unityWebRequest.GetResponseHeader("date");
                    internetTime = DateTime.Parse(dateStr);
                }
                else
                {
                    string json = JsonUtility.FromJson<string>(unityWebRequest.downloadHandler.text);
                    internetTime = DateTime.Parse(json);
                }
                Debug.Log("Got time from " + url + ": " + internetTime.ToString());
                gotInternetTime.Broadcast();

                yield break;
            }
            else
            {
                Debug.Log($"Failed to get time from {url}, trying next server...");
            }
            
        }

        checkingConnectionText.text = "CONNECTION\nERROR";
    }

    void CheckingPlayerDaily()
    {
        if (internetTime.Date.CompareTo(GameManager.Instance.playerData.lastDailyPrizeDate.Date) > 0)
        {
            curWheel.interactable = true;
            checkingConnectionText.gameObject.SetActive(false);
        }
        else
        {
            checkingConnectionText.text = "TODAY PRIZE\nRECEIVED";
        }
    }

    public void SpinTheWheel()
    {
        if (isSpinning) return;

        isSpinning = true;
        int randomPrizeIndex = UnityEngine.Random.Range(0, 8);
        float targetRotation = 20 * 360f + 22.5f * randomPrizeIndex;


        curWheel.gameObject.GetComponent<Image>().rectTransform.DOLocalRotate(new Vector3(0, 0, -targetRotation), 5, RotateMode.FastBeyond360).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                DailyPrize instance = Instantiate(prizeList[randomPrizeIndex]);
                instance.GivePlayerReward();
            });
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.GoToScene("Main");
    }
}
