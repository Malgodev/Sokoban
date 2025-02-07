using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrushController : MonoBehaviour
{
    [field: SerializeField] public EItemType brushType { get; private set; }

    bool isSelected;

    [SerializeField] private Sprite normalBox;
    [SerializeField] private Sprite tickedBox;

    private void ChangeSelectedBox(bool isSelected)
    {
        this.isSelected = isSelected;

        SetPosition();
    }

    private void SetPosition()
    {
        int delta = isSelected ? 1 : -1;

        this.transform.position = this.transform.position + (new Vector3(5, 5, 0) * delta);
    }

    private void ChangeSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = isSelected ? tickedBox : normalBox;
    }
}
