using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fall", menuName = "Bird/States SO/Fall SO")]
public class FallStateSO : StateNode
{
    Bird bird;
    float boundLeftX;
    float boundRightX;
    public override void Enter()
    {
        bird = (Bird)unit;
        boundLeftX = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(0, 0)).x;
        boundRightX = WorldGrid.Instance.GetCellToWorldPosition(new Vector2Int(WorldGrid.Instance.boundCellX - 1, 0)).x;
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        if (bird.transform.position.x < boundLeftX || bird.transform.position.x > boundRightX)
        {
            Vector3 safeZone = bird.transform.position;
            safeZone.x = Mathf.Clamp(safeZone.x, boundLeftX, boundRightX);
            bird.transform.position = safeZone;
        }

        if (bird.isTouchingGround)
        {
            bird.stateMachine.TransitionTo(bird.stateStorage[State.Idle]);
        }
    }

    public override void Exit()
    {

    }
}
