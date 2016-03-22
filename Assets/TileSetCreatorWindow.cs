using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class TileSetCreatorWindow : EditorWindow
{
    TileSet ts;
    public void init()
    {
        Grid g = (Grid)FindObjectOfType(typeof(Grid));
        ts = g.tileSet;
    }

    void OnGUI()
    {
        float x = 30f;
        float y = 20f;
        int row = 1;
        int column = 1;
        if (ts != null)
        {
            //   EditorGUILayout.BeginScrollView();
            foreach (Tile ti in ts.prefabs)
            {
                GameObject prefab = ti.prefab.gameObject;
                Sprite toShow = prefab.GetComponent<SpriteRenderer>().sprite;
                Texture t = toShow.texture;
                Rect tr = toShow.textureRect;

                Rect r = new Rect(tr.x / t.width, tr.y / t.height, tr.width / t.width, tr.height / t.height);

                GUI.DrawTextureWithTexCoords(new Rect(x, y, tr.width, tr.height), t, r);

                x += tr.width + 50;
                column++;
                if (column == 5)
                {
                    row++;
                    column = 1;
                    y += tr.height + 100;
                    x = 30f;
                }


            }
            if(GUI.Button(new Rect(x, y, 60, 20), "New Tile"))
            {
                CreateTileWindow();
            }
        }
    }

    private void CreateTileWindow()
    {
        TileCreatorWindow window = (TileCreatorWindow)EditorWindow.GetWindow(typeof(TileCreatorWindow));
        window.init();

    }
}
