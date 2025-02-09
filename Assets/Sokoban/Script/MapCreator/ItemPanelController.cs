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

            if (tmpBrush == null)
            {
                continue;
            }

            Button button = child.GetComponent<Button>();

            button.onClick.AddListener(() => SelectBrush(tmpBrush.brushType));

            brushList.Add(child.GetComponent<BrushController>());   
        }

        SelectBrush(curItemType);
    }

    private void SelectBrush(EItemType itemType)
    {
        BrushController oldBrush = GetBrush(curItemType);
        DeselectBrush(oldBrush);

        curItemType = itemType;

        BrushController curBrush = GetBrush(itemType);

        if (curBrush == null)
        {
            Debug.Log("????");

            return;
        }

        curBrush.ChangeSelectedBox(true);
    }

    private void DeselectBrush(BrushController oldBrush)
    {
        oldBrush.ChangeSelectedBox(false);
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
}