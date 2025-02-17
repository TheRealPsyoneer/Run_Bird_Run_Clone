using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fall", menuName = "Bird/States SO/Fall SO")]
public class FallStateSO : StateNode
{
    Bird bird;
    public override void Enter()
    {
        bird = (Bird)unit;
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        if (bird.isTouchingGround)
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Idle]);
        }
    }

    public override void Exit()
    {

    }
}
