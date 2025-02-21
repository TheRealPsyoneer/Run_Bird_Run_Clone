using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkinItem : MonoBehaviour
{
    public int thisItemID;
    public EventSO unlockedEvent { get; set; }

    protected virtual void Awake()
    {
        unlockedEvent = ScriptableObject.CreateInstance<EventSO>();
    }

    public abstract bool CheckUnlocked();
}
