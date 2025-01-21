using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject wallPrefab;

    private void Start()
    {
        ParseJsonToMap("TestMaps/1");
    }

    public void ParseJsonToMap(string fileLocaiton)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileLocaiton);

        if (jsonFile != null)
        {
            MapObject mapObject = JsonUtility.FromJson<MapObject>(jsonFile.text);


            GenerateMap(mapObject);
        }

    }

    public void GenerateMap(MapObject mapObject)
    {

        GenerateWall(mapObject.OuterWallTurnPoint.Select(pos => new Vector2(pos.x, pos.y)).ToList());
    }

    public void GenerateWall(List<Vector2> wallPoints)
    {
        wallPoints.Add(wallPoints[0]);

        for (int i = 1; i < wallPoints.Count; i++)
        {
            Vector2[] wallLine = GetLineBetweenPoints(wallPoints[i - 1], wallPoints[i]);

            foreach (Vector2 wallPoint in wallLine)
            {
                GameObject curWall = Instantiate(wallPrefab, wallPoint, Quaternion.identity);
            }
        }
    }

    public Vector2[] GetLineBetweenPoints(Vector2 start, Vector2 end)
    {
        Vector2 tmp = start;
        List<Vector2> result = new List<Vector2>();

        while (tmp != end)
        {
            result.Add(tmp);
            if (tmp.x != end.x)
            {
                tmp.x += tmp.x < end.x ? 1 : -1;
            }

            if (tmp.y != end.y)
            {
                tmp.y += tmp.y < end.y ? 1 : -1;
            }
        }

        return result.ToArray();
    }
}