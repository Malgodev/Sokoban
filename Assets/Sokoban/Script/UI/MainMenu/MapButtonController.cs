using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonController : MonoBehaviour
{
    [SerializeField] public int MapId { get; private set; }
    [SerializeField] public string MapAuthor { get; private set; }


    [Header("UI Component")]
    [SerializeField] private TMP_Text levelId;
    [SerializeField] private Button levelButton;

    private void Start()
    {
        levelButton.onClick.AddListener(
            () => LevelManager.Instance.LoadLevel(MapId, MapAuthor));
    }

    public void SetMapInfo(int mapId, string mapAuthor)
    {
        MapId = mapId;
        MapAuthor = mapAuthor;

        levelId.text = MapId.ToString();
    }
}
