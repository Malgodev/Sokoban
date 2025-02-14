using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatorManager : MonoBehaviour
{
    [SerializeField] public static CreatorManager Instance { get; private set; }

    [Header("Sprite")]
    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite floorSprite;
    [SerializeField] private Sprite playerSpawnSprite;
    [SerializeField] private Sprite boxSprite;
    [SerializeField] private Sprite boxTargetSprite;

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

    void Start()
    {
        
    }

    void ConvertToJson()
    {

    }

    public Sprite GetSpriteByType(EItemType type)
    {
        switch (type)
        {
            case EItemType.Wall:
                return wallSprite;
            case EItemType.Floor:
                return floorSprite;
            case EItemType.PlayerSpawn:
                return playerSpawnSprite;
            case EItemType.Box:
                return boxSprite;
            case EItemType.BoxTarget:
                return boxTargetSprite;
            default:
                return null;
        }
    }
}
