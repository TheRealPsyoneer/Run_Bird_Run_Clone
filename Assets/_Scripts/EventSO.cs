using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event SO")]
public class EventSO : ScriptableObject
{
    public Action<BoxBehaviour> ThingHappened;

    public void Broadcast(BoxBehaviour box)
    {
        ThingHappened?.Invoke(box);
    }
}
