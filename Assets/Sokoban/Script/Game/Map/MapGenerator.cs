using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
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
        }
    }

    public void MapGenerate(MapObject mapObject)
    {

    }

}
