using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    Wall,
    Floor,
    PlayerSpawn,
    Box,
    BoxTarget,
}

public class ItemPanelController : MonoBehaviour
{

    [SerializeField] List<ItemType> items = new List<ItemType>();


    private void Start()
    {
        foreach (ItemType item in items)
        {
            Debug.Log(item);

        }

        //foreach (ItemType value in Enum.GetValues(typeof(ItemType)))
        //{
        //    Debug.Log(value.ToString());
        //}
    }
}

[Serializable]
public struct ItemType
{
    public EItemType itemType;
    public Sprite itemSprite;
    public string objectTag;

    public override string ToString()
    {
        return $"{itemType} {itemSprite} {objectTag}";
    }
}