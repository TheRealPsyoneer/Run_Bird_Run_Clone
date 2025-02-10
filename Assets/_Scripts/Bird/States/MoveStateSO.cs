using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Bird/States SO/Move SO")]
public class MoveStateSO : StateNode
{
    Bird bird;
    public override void Enter()
    {
        bird = (Bird)unit;
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        bird.rb.velocity = bird.direction * bird.curSpeed;
        if (bird.curRotateSpeed != 0)
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Rotate]);
        }
        else if (bird.curSpeed < 0.1f)
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Idle]);
        }
    }

    public override void Exit()
    {

    }
}
