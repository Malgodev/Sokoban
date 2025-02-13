using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectController : MonoBehaviour
{
    public EItemType type { get; private set; }
    public int layer { get; private set; }

    // List of sprite

    private void SetSprite(Sprite sprite)
    {
        this.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SetObjectInfo(EItemType type, int layer)
    {
        this.type = type;
        this.layer = layer;


    }

    public void ChangeSprite()
    {
        // GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
