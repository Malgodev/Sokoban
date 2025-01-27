using System.Collections.Generic;
using System.Linq;
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
    public List<BoxController> BoxControllerList { get; private set; }

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

    public void SetBoxController(List<BoxController> boxControllers)
    {
        BoxControllerList = boxControllers;
    }

    public void CheckWinning()
    {
        foreach (BoxController boxController in BoxControllerList)
        {
            foreach (Vector2 targetPos in TargetPosition)
            {
                if ((Vector2) boxController.transform.position == targetPos)
                {
                    boxController.ChangeColor(true);
                }
                else
                {
                    boxController.ChangeColor(false);
                }
            }
        }
    }
}
