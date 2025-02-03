 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenuController : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button returnButton;
    [SerializeField] private Button normalMapButton;
    [SerializeField] private Button onlineMapButton;
    [SerializeField] private Button createMapButton;

    [Header("Prefab")]
    [SerializeField] private GameObject mapButtonPrefab;

    [Header("Transform")]
    [SerializeField] private Transform mapHolder;

    private void Start()
    {
        LoadNormalMap();

        normalMapButton.onClick.AddListener(LoadNormalMap);
        onlineMapButton.onClick.AddListener(LoadOnlineMap);
    }

    private void LoadNormalMap()
    {
        TextAsset[] files = Resources.LoadAll<TextAsset>("TestMaps");
        foreach (TextAsset file in files)
        {
            for (int i = 0; i < 50; i++)
            {
                SpawnMapButton(int.Parse(file.name));
            }
        }
    }

    private void LoadOnlineMap()
    {

    }

    private void SpawnMapButton(int mapName)
    {
        GameObject mapButton = Instantiate(mapButtonPrefab, mapHolder);

        mapButton.GetComponent<MapButtonController>().SetMapInfo(mapName, "admin");

        // mapButton.transform.SetParent(mapHolder, true);
    }
}
