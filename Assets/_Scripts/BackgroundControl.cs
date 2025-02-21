using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackgroundControl : MonoBehaviour
{
    public static BackgroundControl Instance;

    [SerializeField] EventSO cameraPositionChangeEvent;
    [SerializeField] List<float> moveScale;
    Transform[] objects;

    public List<SpriteRenderer> spriteToBeChanged;
    public List<SpriteRenderer> colorToBeChanged;
    public List<ThemePseudoList> themeLists;
    [Serializable]
    public struct ThemePseudoList
    {
        public List<Sprite> sprites;
        public List<Color> colors;
    }

    private void Awake()
    {
        Instance = this;
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

    private void Start()
    {
        //if (GameManager.Instance.playerData.isFirstTime)
        //{
        //    for (int i = 0; i < spriteToBeChanged.Count; i++)
        //    {
        //        spriteToBeChanged[i].sprite = themeLists[i].sprites[0];
        //    }
        //    for (int i = 0; i < colorToBeChanged.Count; i++)
        //    {
        //        colorToBeChanged[i].color = themeLists[i].colors[0];
        //    }
        //}
        //else
        //{
            for (int i = 0; i < spriteToBeChanged.Count; i++)
            {
                spriteToBeChanged[i].sprite = themeLists[GameManager.Instance.playerData.themeID].sprites[i];
            }
            for (int i = 0; i < colorToBeChanged.Count; i++)
            {
                colorToBeChanged[i].color = themeLists[GameManager.Instance.playerData.themeID].colors[i];
            }
        //}
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
