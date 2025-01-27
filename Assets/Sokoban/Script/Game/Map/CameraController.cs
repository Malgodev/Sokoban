using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void SetCameraPosition(int maxX, int maxY)
    {
        Camera cam = GetComponent<Camera>();

        this.transform.position = new Vector3 (maxX / 2, maxY / 2, -10);
        cam.orthographicSize = Mathf.Max(maxX, maxY) / 2 + 1;
    }
}
