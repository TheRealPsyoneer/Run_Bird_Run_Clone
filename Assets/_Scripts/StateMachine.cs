using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Unit unit;
    public StateNode currentState;

    public StateMachine(Unit owner, StateNode entryState)
    {
        unit = owner;
        currentState = entryState;
        currentState.unit = unit;
        currentState.Enter();
    }

    public void TransitionTo(StateNode nextState)
    {
        currentState.Exit();
        currentState = nextState;
        currentState.unit = unit;
        currentState.Enter();
    }

    public void FixedExecute()
    {
        currentState?.FixedExecute();
    }

    public void Execute()
    {
        currentState?.Execute();
    }
}
