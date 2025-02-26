using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockWithChallenge : UnlockableItem
{
    int challengeThreshold;
    TextMeshProUGUI challengeText;

    protected override void Awake()
    {
        base.Awake();
        challengeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();
        challengeThreshold = int.Parse(challengeText.text);
        UnlockAction();
    }

    public override void UnlockAction()
    {
        if (unlockableItem.CheckUnlocked()) return;

        if (GameManager.Instance.playerData.challengeNumber >= challengeThreshold)
        {
            AudioManager.Instance.PlayAudioClip("ItemUnlock");
            unlockableItem.unlockedEvent.Broadcast();
            unlockConditionCover.DOFade(0, 1).OnComplete(() => unlockConditionCover.gameObject.SetActive(false));
        }
    }

    public void RedirectToChallenges()
    {
        AudioManager.Instance.PlayAudioClip("Error");
        GameManager.Instance.GoToScene("Challenges");
    }
}
