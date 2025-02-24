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
    public Button curWheel { get; set; }

    [SerializeField] TextMeshProUGUI checkingConnectionText;
    public DateTime internetTime;
    public EventSO gotInternetTime { get; set; }

    bool isSpinning;
    public List<DailyPrize> prizeList1;
    public List<DailyPrize> prizeList2;
    public List<DailyPrize> curPrizeList { get; set; }

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

            curPrizeList = prizeList1;
        }
        else
        {
            curWheel = wheel2;
            curWheel.gameObject.SetActive(true);
            wheel1.gameObject.SetActive(false);

            curPrizeList = prizeList2;
        }

        curWheel.gameObject.GetComponent<CanvasGroup>().alpha = 0.25f;
        curWheel.interactable = false;

        StartCoroutine(GettingInternetTime());
    }

    IEnumerator GettingInternetTime()
    {
        string timeURLs = "http://time.google.com";

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(timeURLs);
        unityWebRequest.timeout = 5;

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.Success)
        {
            string dateStr = unityWebRequest.GetResponseHeader("date");
            internetTime = DateTime.Parse(dateStr);

            Debug.Log("Got time from " + timeURLs + ": " + internetTime.ToString());
            gotInternetTime.Broadcast();
        }
        else
        {
            Debug.Log($"Failed to get time from {timeURLs}, trying next server...");
            checkingConnectionText.text = "CONNECTION\nERROR";
        }
    }

    void CheckingPlayerDaily()
    {
        if (internetTime.Date.CompareTo(GameManager.Instance.playerData.lastDailyPrizeDate.Date) > 0)
        {
            curWheel.interactable = true;
            curWheel.gameObject.GetComponent<CanvasGroup>().alpha = 1;
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
        float targetRotation = 15 * 360f + UnityEngine.Random.Range(1f,44f) + 45f * randomPrizeIndex;


        curWheel.gameObject.GetComponent<Image>().rectTransform.DOLocalRotate(new Vector3(0, 0, targetRotation), 5, RotateMode.FastBeyond360).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                DailyPrize instance = Instantiate(curPrizeList[randomPrizeIndex]);
                instance.GivePlayerReward();
                GameManager.Instance.playerData.lastDailyPrizeDate = internetTime;
                isSpinning = false;
            });
    }

    public void ReturnToMenu()
    {
        if (isSpinning) return;
        GameManager.Instance.GoToScene("Main");
    }
}
