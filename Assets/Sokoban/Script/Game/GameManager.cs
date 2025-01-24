using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

enum ObjectType
{
    Box,
    Wall,
    Floor,
    BoxTarget
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerMovementController PlayerMovementController;

    public List<Vector2> TargetPosition { get; private set; }
    public List<BoxController> BoxControllers { get; private set; }

    public const float MOVE_TIME = .5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        TargetPosition = new List<Vector2>();
    }

    public void SetPlayerDefaultPosition(Vector2 defaultPosition)
    {
        PlayerMovementController.SetDefaultPosition(defaultPosition);


    }

    public void SetTargetPosition(Vector2[] targetPosition)
    {
        TargetPosition = targetPosition.ToList();
    }

    public void CheckWinning()
    {
        foreach (Vector2 targetPos in TargetPosition)
        {
            Collider2D box = Utility.OverlapPoint(targetPos, "Box");
            if (box)
            {
                box.GetComponent<BoxController>().ChangeColor(true);
            }
        }
    }
}
