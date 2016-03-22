using UnityEngine;
using System.Collections;
using UnityEditor;

public class TileCreatorWindow : EditorWindow
{
    TileSet ts;
    public void init()
    {
        Grid g = (Grid)FindObjectOfType(typeof(Grid));
        ts = g.tileSet;
        tile = new Tile();

    }
    public bool finished;
    public Tile tile;
    void OnGUI()
    {
      
    }

    void Finish()
    {
        
    }
}
