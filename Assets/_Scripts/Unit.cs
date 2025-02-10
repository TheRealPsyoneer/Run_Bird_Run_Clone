using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] List<StateNode> states;
    public Dictionary<State, StateNode> stateStorage;
    public StateMachine stateMachine;

    protected virtual void Awake()
    {
        stateStorage = new();
        foreach (StateNode stateNode in states)
        {
            stateStorage[stateNode.state] = stateNode;
        }
        stateMachine = new(this, stateStorage[State.Idle]);
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedExecute();
    }

    protected virtual void Update()
    {
        stateMachine.Execute();
    }
}
