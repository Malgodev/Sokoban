using System.Collections;
using System.Collections.Generic;
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

    public const float MOVE_TIME = 1f;

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
    }

    public void SetPlayerDefaultPosition(Vector2 defaultPosition)
    {
        PlayerMovementController.SetDefaultPosition(defaultPosition);
    }
}
