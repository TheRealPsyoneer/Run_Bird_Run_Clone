using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Die", menuName = "Bird/States SO/Die SO")]
public class DieStateSO : StateNode
{
    Bird bird;
    public override void Enter()
    {
        bird = (Bird)unit;

        bird.transform.DOScaleY(0, BoxBehaviour.FallTimePerCell)
            .OnComplete(() => bird.gameObject.SetActive(false));
        bird.dieEvent.Broadcast(bird);
    }

    public override void FixedExecute() { }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {

    }
}
