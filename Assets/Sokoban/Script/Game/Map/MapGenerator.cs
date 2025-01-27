using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform enviroment;

    [Header("Prefab")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject targetBoxPrefab;

    private int maxX;
    private int maxY;

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
        // Generate outer wall
        GenerateWall(GetVectorArrayFromPoints(mapObject.OuterWallTurnPoint).ToList(), out maxX, out maxY);

        // Generate inner wall
        for (int i = 0; i < mapObject.InnerWallTurnPoint.Length; i++)
        {
            GenerateWall(mapObject.InnerWallTurnPoint[i].Points);
        }

        // Generate floor
        GenerateFloor(maxX, maxY);

        // Generate box
        GameObject[] boxObjectList = GeneratePrefabAtPosition(mapObject.BoxPosition, boxPrefab);

        List<BoxController> boxControllers = boxObjectList.Select(x => x.GetComponent<BoxController>()).ToList();
        
        GameManager.Instance.SetBoxController(boxControllers);

        // Generate target box position
        GeneratePrefabAtPosition(mapObject.TargetPosition, targetBoxPrefab);
        GameManager.Instance.SetTargetPosition (GetVectorArrayFromPoints(mapObject.TargetPosition));

        // Setplayer position
        GameManager.Instance.SetPlayerDefaultPosition(mapObject.PlayerPosition.Vector);

        GameManager.Instance.CheckWinning();
        GameManager.Instance.SetupCamera(maxX, maxY);
    }

    private void GenerateFloor(int maxX, int maxY)
    {
        for (int y = 0; y <= maxY; y++)
        {
            int leftWallIndex = 0;
            int rightWallIndex = maxX;

            bool hasLeftWall = false;
            bool hasRightWall = false;

            while (leftWallIndex <= rightWallIndex)
            {
                hasLeftWall = HasWall(new Vector2(leftWallIndex, y));
                hasRightWall = HasWall(new Vector2(rightWallIndex, y));

                if (hasLeftWall && hasRightWall)
                {
                    break;
                }

                leftWallIndex++;
                rightWallIndex--;
            }

            if (!(hasLeftWall && hasRightWall))
            {
                continue;
            }

            for (int x = leftWallIndex; x <= rightWallIndex; x++)
            {
                Vector2 targetPoint = new Vector2(x, y);
                if (!HasWall(targetPoint))
                {
                    GameObject curFloor = Instantiate(floorPrefab, targetPoint, Quaternion.identity, enviroment);
                }
            }
        }
    }

    private bool HasWall(Vector2 point)
    {
        // TODO Hard code
        Collider2D wall = Utility.OverlapPoint(point, "Wall");

        return wall != null;
    }

    public GameObject[] GeneratePrefabAtPosition(Position[] position, GameObject prefab)
    {
        List<GameObject> prefabList = new List<GameObject>();

        foreach (Position pos in position)
        {
            GameObject curBox = Instantiate(prefab, pos.Vector, Quaternion.identity, enviroment);
            prefabList.Add(curBox);
        }

        return prefabList.ToArray();
    }

    public void GenerateWall(Position[] turnPoint)
    {
        List<Vector2> wallPoints = GetVectorArrayFromPoints(turnPoint).ToList();

        GenerateWall(wallPoints);
    }

    public void GenerateWall(List<Vector2> wallPoints)
    {
        wallPoints.Add(wallPoints[0]);

        for (int i = 1; i < wallPoints.Count; i++)
        {
            Vector2[] wallLine = GetLineBetweenPoints(wallPoints[i - 1], wallPoints[i]);

            if (wallLine.Length == 0)
            {
                GameObject curWall = Instantiate(wallPrefab, wallPoints[i], Quaternion.identity, enviroment);
            }

            foreach (Vector2 wallPoint in wallLine)
            {
                GameObject curWall = Instantiate(wallPrefab, wallPoint, Quaternion.identity, enviroment);
            }
        }
    }

    public void GenerateWall(List<Vector2> wallPoints, out int maxX, out int maxY)
    {
        maxX = 0;
        maxY = 0;

        wallPoints.Add(wallPoints[0]);

        for (int i = 1; i < wallPoints.Count; i++)
        {
            maxX = Mathf.Max(maxX, (int) wallPoints[i].x, (int) wallPoints[i - 1].x);
            maxY = Mathf.Max(maxY, (int) wallPoints[i].y, (int) wallPoints[i - 1].y);

            Vector2[] wallLine = GetLineBetweenPoints(wallPoints[i - 1], wallPoints[i]);

            if (wallLine.Length == 0)
            {
                GameObject curWall = Instantiate(wallPrefab, wallPoints[i], Quaternion.identity, enviroment);
            }

            foreach (Vector2 wallPoint in wallLine)
            {
                GameObject curWall = Instantiate(wallPrefab, wallPoint, Quaternion.identity, enviroment);
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

    public static Vector2[] GetVectorArrayFromPoints(Position[] points)
    {
        return points.Select(pos => new Vector2(pos.x, pos.y)).ToArray();
    }
}