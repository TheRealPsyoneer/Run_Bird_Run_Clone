using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Bird/States SO/Move SO")]
public class MoveStateSO : StateNode
{
    Bird bird;
    float boundLeftX;
    float boundRightX;

    [SerializeField] float sweatInterval;
    [SerializeField] float dustInterval;
    float initFrameSweat;
    float initFrameDust;
    bool activateSweat2;


    public override void Enter()
    {
        bird = (Bird)unit;
        boundLeftX = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(0, 0)).x;
        boundRightX = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(WorldGrid.Instance.boundCellX-1, 0)).x;

        bird.animator.SetBool("IsRunning", true);

        initFrameSweat = Time.time;
        activateSweat2 = false;
        initFrameDust = 0;
        bird.sweat.gameObject.SetActive(true);
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        bird.direction = bird.directions[bird.directionIndex];

        if (bird.curSpeed < 0.1f)
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Idle]);
            return;
        }

        if (Time.time - initFrameSweat >= sweatInterval && !activateSweat2)
        {
            bird.sweat2.gameObject.SetActive(true);
            activateSweat2 = true;
        }

        if (Time.time - initFrameDust >= dustInterval)
        {
            bird.expression.CreateDust(bird.transform);
            initFrameDust = Time.time;
        }

        if (bird.direction == Vector2.up)
        {
            bird.transform.Translate(bird.direction * Time.deltaTime * 1f * bird.curSpeed);
        }
        else
        {
            bird.transform.Translate(bird.direction * Time.deltaTime * bird.curSpeed);
        }

        if (bird.transform.position.x <= boundLeftX)
        {
            bird.transform.position = new Vector3(boundLeftX, bird.transform.position.y, bird.transform.position.z);
        }
        if (bird.transform.position.x >= boundRightX)
        {
            bird.transform.position = new Vector3(boundRightX, bird.transform.position.y, bird.transform.position.z);
        }

        //bird.rb.velocity = bird.direction * bird.curSpeed;
        //if (bird.curRotateSpeed != 0)
        //{
        //    bird.stateMachine.TransitionTo(bird.stateStorage[State.Rotate]);
        //}

    }

    public override void Exit()
    {
        bird.animator.SetBool("IsRunning", false);
        bird.sweat.gameObject.SetActive(false) ;
        bird.sweat2.gameObject.SetActive(false);
    }
}
