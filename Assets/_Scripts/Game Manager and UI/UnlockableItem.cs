using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnlockableItem : MonoBehaviour
{
    public SkinItem unlockableItem { get; set; }

    public CanvasGroup unlockConditionCover { get; set; }

    protected virtual void Awake()
    {
        unlockableItem = GetComponentInParent<SkinItem>();
        unlockConditionCover = GetComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        if (unlockableItem.CheckUnlocked())
        {
            unlockConditionCover.alpha = 0;
            unlockConditionCover.blocksRaycasts = false;
        }
        else
        {
            unlockConditionCover.alpha = 1;
            unlockConditionCover.blocksRaycasts = true;
        }
    }

    public abstract void UnlockAction();
}
