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
        GameManager.Instance.GoToScene("Daily");
    }
}
