using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName ="Idle",menuName = "Bird/States SO/Idle SO")]
public class IdleStateSO : StateNode
{
    Bird bird;
    [SerializeField] float rotatePerDegreeTime;
    public override void Enter()
    {
        bird = (Bird)unit;

        float curBirdRotation = bird.children[1].transform.rotation.z;
        if (curBirdRotation != 0)
        {
            bird.children[1].transform.DOKill();
            bird.children[1].transform.DOLocalRotate(Vector3.zero, curBirdRotation * rotatePerDegreeTime).SetEase(Ease.Linear);
        }
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        if (bird.curSpeed != 0 && bird.isTouchingGround)
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Move]);
        }
    }

    public override void Exit()
    {
        
    }
}
