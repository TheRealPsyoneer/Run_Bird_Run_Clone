using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Rotate", menuName = "Bird/States SO/Rotate SO")]
public class RotateStateSO : StateNode
{
    Bird bird;
    [SerializeField] float rotateTime;
    public override void Enter()
    {
        bird = (Bird)unit;
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        bird.transform.DORotate(new Vector3(0, 0, 90), rotateTime);
        if (bird.curRotateSpeed == 0)
        {
            bird.transform.DOKill();
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Move]);
        }
        else if (bird.curSpeed < 0.1f)
        {
            bird.transform.DOKill();
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Idle]);
        }
    }

    public override void Exit()
    {

    }
}
