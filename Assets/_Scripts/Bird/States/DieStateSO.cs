using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Die", menuName = "Bird/States SO/Die SO")]
public class DieStateSO : StateNode
{
    Bird bird;
    public override void Enter()
    {
        bird = (Bird)unit;
        bird.curSpriteRenderer.sprite = bird.spriteLists[GameManager.Instance.playerData.birdID].sprites[0];

        bird.transform.DOScaleY(0, BoxBehaviour.FallTimePerCell).SetUpdate(true)
            .OnComplete(BirdDie);
    }

    void BirdDie()
    {
        bird.gameObject.SetActive(false);
        bird.dieEvent.Broadcast(bird);
        bird.dieEvent.Broadcast();
    }

    public override void FixedExecute() { }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }
}
