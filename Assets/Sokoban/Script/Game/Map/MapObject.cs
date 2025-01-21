using System.Linq;
using UnityEngine;

[System.Serializable]
public class MapObject
{
    public string MapId;
    public Position[] OuterWallTurnPoint;
    public TurnPoint[] InnerWallTurnPoint;
    public Position[] BoxPosition;
    public Position[] TargetPosition;
    public Position PlayerPosition;
}

[System.Serializable]
public class TurnPoint
{
    public Position[] Points;

    public override string ToString()
    {
        return Points.Length.ToString();
    }
}

[System.Serializable]
public class Position
{
    public int x;
    public int y;

    public Vector2 Vector
    {
        get
        {
            return new Vector2(x, y);
        }
    }

    public override string ToString()
    {
        return x + ":" + y;
    }

    public Vector3 GetVector()
    {
        return new Vector3(x, y, 0); 
    }
}