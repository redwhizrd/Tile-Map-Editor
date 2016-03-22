using UnityEngine;

using System.Collections;
using System;

public class Grid : MonoBehaviour
{
    public float Height = 32.0f;
    public float Width = 32.0f;
    public Color color = Color.white;
    public bool locked;
    public Tile tilePrefab;
    public TileSet tileSet;
    void OnDrawGizmos()
    {
      
        Vector3 pos = Camera.main.transform.position;
        Gizmos.color = color;
        
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        //changes horizontal lines
        for (float y = pos.y - cameraHeight+Height; y <= pos.y + cameraHeight; y += Height)
        {
            Gizmos.DrawLine(new Vector3(cameraWidth * -1, Mathf.Floor(y / Height) * Height, 0.0f),
                            new Vector3(cameraWidth, Mathf.Floor(y / Height) * Height, 0.0f));

        }
        //changes vertical lines
       
        for (float x = pos.x - cameraWidth+ Width; x < pos.x + cameraWidth; x += Width)
        {
            Gizmos.DrawLine(new Vector3(Mathf.Floor(x / Width) * Width, cameraHeight * -1, 0.0f),
                            new Vector3(Mathf.Floor(x / Width) * Width, cameraHeight, 0.0f));

        }

    }

    public void UpdateGrid()
    {
      
    }
}
