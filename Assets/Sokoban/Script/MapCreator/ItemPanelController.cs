using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EItemType
{
    Wall,
    Floor,
    PlayerSpawn,
    Box,
    BoxTarget,
    Eraser,
}

public class ItemPanelController : MonoBehaviour
{

    EItemType curItemType;

    [SerializeField] List<BrushController> brushList = new List<BrushController>();

    private void Start()
    {
        foreach (Transform child in this.transform)
        {
            BrushController tmpBrush = child.GetComponent<BrushController>();
            Button button = child.GetComponent<Button>();

            button.onClick.AddListener(() => SelectBrush(tmpBrush.brushType));


            brushList.Add(child.GetComponent<BrushController>());   
        }
    }

    private void SelectBrush(EItemType itemType)
    {
        DeselectBrush();

        curItemType = itemType;

        BrushController curBrush = GetBrush(itemType);

        if (curBrush == null)
        {
            Debug.Log("????");

            return;
        }
        else
        {
            Debug.Log(curBrush.brushType.ToString());
        }

        switch (itemType)
        {
            case EItemType.Wall:
                break;


        }
    }

    private BrushController GetBrush(EItemType itemType)
    {
        foreach (BrushController brush in brushList)
        {
            if (brush.brushType == itemType)
            {
                return brush;
            }
        }

        return null;
    }

    private void DeselectBrush()
    {

    }

    private void ChangeBrushBoxUI(GameObject brushBox)
    {

    }
}