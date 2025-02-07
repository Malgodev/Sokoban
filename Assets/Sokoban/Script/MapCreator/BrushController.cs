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
    [SerializeField] private RectTransform insideRect;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        foreach (Transform child in this.transform)
        {
            insideRect = child.GetComponent<RectTransform>();
        }
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
        rectTransform.anchoredPosition = rectTransform.anchoredPosition + (new Vector2(5, 10) * deltaPosition);

        int deltaScale = isSelected ? 160 : 150;
        rectTransform.sizeDelta = new Vector2(deltaScale, deltaScale);

        insideRect.anchoredPosition = isSelected ? new Vector2(-2.5f, -5) : new Vector2(0, 0);
    }

    private void ChangeSprite()
    {
        this.GetComponent<Image>().sprite = isSelected ? tickedBox : normalBox;
    }
}
