using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Idle",menuName = "Bird/States SO/Idle SO")]
public class IdleStateSO : StateNode
{
    Bird bird;
    public override void Enter()
    {
        bird = (Bird)unit;
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        if (bird.curSpeed != 0)
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Move]);
        }
    }

    public override void Exit()
    {
        
    }
}
