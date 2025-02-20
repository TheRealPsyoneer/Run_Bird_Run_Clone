using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    [SerializeField] EventSO cameraPositionChangeEvent;
    [SerializeField] List<float> moveScale;
    Transform[] objects;

    private void Awake()
    {
        objects = GetComponentsInChildren<Transform>();
    }

    private void OnEnable()
    {
        cameraPositionChangeEvent.ThingHappened += MoveCameraAndBackground;
    }

    private void OnDisable()
    {
        cameraPositionChangeEvent.ThingHappened -= MoveCameraAndBackground;
    }

    void MoveCameraAndBackground()
    {
        Camera.main.transform.DOMoveY(Camera.main.transform.position.y + WorldGrid.Instance.CelValue, 1);
        for (int i = 1; i < objects.Length; i++)
        {
            if (moveScale[i] != 0)
            {
                objects[i].DOLocalMoveY(objects[i].localPosition.y + WorldGrid.Instance.CelValue * moveScale[i], 1);
            }
        }
    }
}
