using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Bird/States SO/Move SO")]
public class MoveStateSO : StateNode
{
    Bird bird;
    float boundLeftX;
    float boundRightX;
    public override void Enter()
    {
        bird = (Bird)unit;
        boundLeftX = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(0, 0)).x;
        boundRightX = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(WorldGrid.Instance.boundCellX-1, 0)).x;
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        //if (!bird.isClimbing)
        //{
        //    bird.directions = bird.normalDirections;
        //}

        bird.direction = bird.directions[bird.directionIndex];

        if (bird.curSpeed < 0.1f 
            || (bird.direction == Vector2.left && bird.transform.position.x < boundLeftX) 
            || (bird.direction == Vector2.right && bird.transform.position.x > boundRightX))
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Idle]);
            return;
        }

        


        if (bird.direction == Vector2.up)
        {
            bird.transform.Translate(bird.direction * Time.deltaTime * 1f * bird.curSpeed);
        }
        else
        {
            bird.transform.Translate(bird.direction * Time.deltaTime * bird.curSpeed);
        }
        //bird.rb.velocity = bird.direction * bird.curSpeed;
        //if (bird.curRotateSpeed != 0)
        //{
        //    bird.stateMachine.TransitionTo(bird.stateStorage[State.Rotate]);
        //}
        
    }

    public override void Exit()
    {

    }
}
