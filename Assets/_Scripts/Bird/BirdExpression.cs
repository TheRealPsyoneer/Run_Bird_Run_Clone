using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdExpression : MonoBehaviour
{
    [SerializeField] MyFactorySO dustFactory;
    [SerializeField] Transform dustPos;

    private void Awake()
    {
        dustFactory.Initialize();
    }

    public DustProduct CreateDust(Transform ownerTransform)
    {
        DustProduct dust = (DustProduct) dustFactory.GetProduct();
        dust.transform.position = dustPos.position;
        return dust;
    }
}
