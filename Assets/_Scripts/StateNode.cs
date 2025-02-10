using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateNode : ScriptableObject
{
    public State state;
    [HideInInspector]
    public Unit unit;
    public abstract void Enter();
    public abstract void FixedExecute();
    public abstract void Execute();
    public abstract void Exit();

}
