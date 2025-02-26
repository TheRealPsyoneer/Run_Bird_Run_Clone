using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockWithSpecial : UnlockableItem
{
    public override void UnlockAction()
    {
        
    }

    public void RedirectToDaily()
    {
        AudioManager.Instance.PlayAudioClip("Error");
        GameManager.Instance.GoToScene("Daily");
    }
}
