using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class MapObjectController : MonoBehaviour
{
    [field: SerializeField] public EItemType type { get; private set; }
    [field: SerializeField] public int layer { get; private set; }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetObjectInfo(EItemType type, int layer)
    {
        this.type = type;
        this.layer = layer;
        spriteRenderer.sortingOrder = layer;
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        Sprite sprite = CreatorManager.Instance.GetSpriteByType(type);
    
        if (sprite == null)
        {
            return;
        }

        spriteRenderer.sprite = sprite;
    }
}
