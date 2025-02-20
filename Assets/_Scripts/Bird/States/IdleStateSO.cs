using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName ="Idle",menuName = "Bird/States SO/Idle SO")]
public class IdleStateSO : StateNode
{
    Bird bird;
    [SerializeField] float rotatePerDegreeTime;
    float boundLeftX;
    float boundRightX;
    public override void Enter()
    {
        bird = (Bird)unit;

        boundLeftX = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(0, 0)).x;
        boundRightX = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(WorldGrid.Instance.boundCellX - 1, 0)).x;

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
            //&& ((bird.direction == Vector2.left && bird.transform.position.x <= boundRightX)
            //|| (bird.direction == Vector2.right && bird.transform.position.x >= boundLeftX)))
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Move]);
        }

        if (bird.transform.position.x <= boundLeftX)
        {
            bird.transform.position = new Vector3(boundLeftX, bird.transform.position.y, bird.transform.position.z);
        }
        if (bird.transform.position.x >= boundRightX)
        {
            bird.transform.position = new Vector3(boundRightX, bird.transform.position.y, bird.transform.position.z);
        }
    }

    public override void Exit()
    {
        
    }
}
