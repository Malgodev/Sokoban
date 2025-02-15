using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] ToggleSwitch brushToggle; // true if erase mode, else pen mode

    [Header("Map Creator")]
    [SerializeField] Transform mapObjectHolder;
    [SerializeField] GameObject mapObjectPrefab;

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

            button.onClick.AddListener(() => {
                SelectBrush(tmpBrush.brushType);
                });

            brushList.Add(child.GetComponent<BrushController>());   
        }

        SelectBrush(curItemType);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!brushToggle.CurrentValue) // pen mode
            {
                WriteAtPosition(Utility.GetWorldPositionAtMouse());
            }
            else // erase mode
            {
                EraseAtPosition(Utility.GetWorldPositionAtMouse());
            }
        }
    }

    private void WriteAtPosition(Vector2 targetPosition)
    {
        targetPosition = new Vector2(
            Mathf.Floor(targetPosition.x) + 0.5f,
            Mathf.Floor(targetPosition.y) + 0.5f);

        int layer;
        GetObjectInfo(curItemType, out layer);

        if (IsWriteable(targetPosition, layer))
        {
            GameObject tmpGameObject = Instantiate(mapObjectPrefab, targetPosition, Quaternion.identity, mapObjectHolder);

            MapObjectController mapObjectController = tmpGameObject.GetComponent<MapObjectController>();
            mapObjectController.SetObjectInfo(curItemType, layer);
        }
    }

    private bool IsWriteable(Vector2 targetPosition, int layer)
    {
        // Check if is in allowed rect (get max location and min location -> check if still in rect)

        // Check if is in UI
        float panelWidth = this.GetComponent<RectTransform>().sizeDelta.x;
        float screenWidth = Screen.width;

        Vector3 mouseViewpot = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (mouseViewpot.x < (panelWidth / screenWidth))
        {
            return false;
        }

        // TODO Hardcode
        // Make a check for layer, if item is (wall, floor => layer 1, else layer 2)
        Collider2D collider = Utility.OverlapPoint(targetPosition, "MapObject");

        if (collider == null)
        {
            return true;
        }

        MapObjectController objectController = collider.GetComponent<MapObjectController>();

        if (objectController == null)
        {
            Debug.LogError("Map object not containing script");

            return false;
        }

        if (objectController.layer < layer)
        {
            return true;
        }

        return false;
    }

    private void GetObjectInfo(EItemType type, out int layer)
    {
        layer = 1;
        switch (type)
        {
            case EItemType.Wall:
                layer = 10;
                break;
            case EItemType.Floor:
                layer = 1;
                break;
            case EItemType.PlayerSpawn:
                layer = 3;
                break;
            case EItemType.Box:
                layer = 3;
                break;
            case EItemType.BoxTarget:
                layer = 2;
                break;
        }
    }

    private void EraseAtPosition(Vector2 targetPosition)
    {
        // TODO Hardcode
        Collider2D collider = Utility.OverlapPoint(targetPosition, "MapObject");
        if (collider)
        {
            Destroy(collider.gameObject);
        }
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