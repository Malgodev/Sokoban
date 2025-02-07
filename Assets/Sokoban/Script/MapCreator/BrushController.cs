using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BrushController : MonoBehaviour
{
    [field: SerializeField] public EItemType brushType { get; private set; }

    bool isSelected;

    [SerializeField] private Sprite normalBox;
    [SerializeField] private Sprite tickedBox;

    [SerializeField] private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ChangeSelectedBox(bool isSelected)
    {
        this.isSelected = isSelected;

        SetTransform();
        ChangeSprite();
    }

    private void SetTransform()
    {
        int deltaPosition = isSelected ? 1 : -1;

        Debug.Log(rectTransform.rect);

        rectTransform.localPosition = rectTransform.localPosition + (new Vector3(5, 10, 0) * deltaPosition);

        Debug.Log(rectTransform.localPosition);

        int deltaScale = isSelected ? 160 : 150;

        rectTransform.sizeDelta = new Vector2(deltaScale, deltaScale);
    }

    private void ChangeSprite()
    {
        this.GetComponent<Image>().sprite = isSelected ? tickedBox : normalBox;
    }
}
