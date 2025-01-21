using System.Numerics;

[System.Serializable]
public class MapObject
{
    public string MapId;
    public Position[] OuterWallTurnPoint;
    public Position[][] InnerWallTurnPoint;
    public Position[] BoxPosition;
    public Position[] TargetPosition;
    public Position PlayerPosition;
}

[System.Serializable]
public class Position
{
    public int x;
    public int y;

    public override string ToString()
    {
        return x + ":" + y;
    }
}