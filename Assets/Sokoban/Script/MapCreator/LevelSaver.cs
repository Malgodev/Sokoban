using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelSaver : MonoBehaviour
{
    [Header("Saver")]
    [SerializeField] private Transform objectHolder;
    [SerializeField] private Button saveBtn;

    void Start()
    {
        saveBtn.onClick.AddListener(SaveLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SaveLevel()
    {
        foreach (Transform child in objectHolder)
        {
            MapObjectController objectController = child.GetComponent<MapObjectController>();

            switch (objectController.type)
            {
                case EItemType.Wall:
                    GetAllNearbyWall(child);

                    break;
            }

            break;
        }
    }

    void GetAllNearbyWall(Transform targetWall)
    {

        int i = 0;

        Transform curWall = targetWall;
        List<Vector2> turnPoint = new List<Vector2>();

        while ((Vector2) curWall.transform.position == turnPoint[0])
        {
            Transform[] nearbyWalls = GetNearbyWall(targetWall);

            if (nearbyWalls.Length == 0)
            {
                turnPoint.Add(curWall.position);
                return;
            }

            // Check if the point is already in turn point list



            Debug.Log(curWall.transform.position);

            if (i++ > 1000)
            {
                break;
            }
        }
    }

    private bool IsInTurnPoint(Vector2[] turnPoints, Vector2 checkPoint)
    {
        for (int i = 0; i < turnPoints.Length; i++)
        {

        }

        //foreach (Vector2 turnPoint in turnPoints)
        //{
        //    float sameAxis = turnPoint.x == turnPoint.

        //    if (turnPoint.x )
        //}

        return true;
    }

    Transform[] GetNearbyWall(Transform targetWall)
    {
        Vector2 downLeftPoint = targetWall.position - new Vector3(1, 1, 0);
        Vector2 upRightPoint = targetWall.position + new Vector3(1, 1, 0);

        Collider2D[] colliders = Physics2D.OverlapAreaAll(downLeftPoint, upRightPoint);

        List<Transform> wallTransform = new List<Transform>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (targetWall == colliders[i].transform)
            {
                continue;
            }

            wallTransform.Add(colliders[i].transform);
        }

        wallTransform.Sort(CompareNearbyWall);

        foreach (Transform t in wallTransform)
        {
            Debug.Log(t.name);
        }

        return wallTransform.ToArray();
    }

    int CompareNearbyWall(Transform a, Transform b)
    {
        if (a.position.x != b.position.x)
            return a.position.x.CompareTo(b.position.x);

        return a.position.y.CompareTo(b.position.y);
    }
}
