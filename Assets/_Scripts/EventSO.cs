using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event SO")]
public class EventSO : ScriptableObject
{
    public Action<BoxBehaviour> ThingHappenedBox;
    public Action<Unit> ThingHappenedUnit;

    public void Broadcast(BoxBehaviour box)
    {
        ThingHappenedBox?.Invoke(box);
    }

    public void Broadcast(Unit unit)
    {
        ThingHappenedUnit?.Invoke(unit);
    }
}
