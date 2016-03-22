using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System;

[CustomEditor(typeof(Grid))]
public class GridScriptEditor : Editor
{
    Grid grid;
    private int oldIndex;
    void OnEnable()
    {
        oldIndex = 0;
        grid = (Grid)target;
    }

    [MenuItem("Assets/Create/TileSet")]
    static void CreateTileSet()
    {
        var asset = ScriptableObject.CreateInstance<TileSet>();
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";

        }
        else if(Path.GetExtension(path) !="")
        {
            path = path.Replace(Path.GetFileName(path), "");
        }
        else
        {
            path += "/";
        }

        var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "TileSet.asset");
        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
        asset.hideFlags = HideFlags.DontSave;

    }
    [MenuItem("Assets/Create/Tile")]
    static void CreateTile()
    {
        var asset = ScriptableObject.CreateInstance<Tile>();
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";

        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(path), "");
        }
        else
        {
            path += "/";
        }

        var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "Tile.asset");
        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
        asset.hideFlags = HideFlags.DontSave;

    }


    public override void OnInspectorGUI()
    {
        grid = (Grid)target;

        if (GUILayout.Button("Open Grid Window"))
        {
            GridWindow window = (GridWindow)EditorWindow.GetWindow(typeof(GridWindow));
            window.init();

        }


        if (GUILayout.Button("Create New Tile Set"))
        {
            TileSetCreatorWindow window = (TileSetCreatorWindow)EditorWindow.GetWindow(typeof(TileSetCreatorWindow));
            window.init();

        }

        //Type prefab
        EditorGUI.BeginChangeCheck();
        var newTilePrefab = (Tile)EditorGUILayout.ObjectField("Tile_Prefab", grid.tilePrefab, typeof(Tile), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Grid Changed");

            grid.tilePrefab = newTilePrefab;
          
        }

        //Tile Map
        EditorGUI.BeginChangeCheck();
        var newTileSet = (TileSet)EditorGUILayout.ObjectField("TileSet", grid.tileSet, typeof(TileSet), false);
        if (EditorGUI.EndChangeCheck())
        {
            grid.tileSet = newTileSet;
            Undo.RecordObject(target, "Grid Changed");
        }

        if (grid.tileSet != null)
        {
            EditorGUI.BeginChangeCheck();
            var names = new string[grid.tileSet.prefabs.Length];
            var values = new int[names.Length];

            for(int i = 0; i < names.Length; i++)
            {
                names[i] = grid.tileSet.prefabs[i].prefab.name != null ? grid.tileSet.prefabs[i].prefab.name : "";
                values[i] = i;
            }
            var index = EditorGUILayout.IntPopup("Select Tile", oldIndex,names,values);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Grid Changed");
                if (oldIndex != index) { 
                    oldIndex = index;
                    grid.tilePrefab = grid.tileSet.prefabs[index];

                    float width = grid.tilePrefab.prefab.GetComponent<Renderer>().bounds.size.x;
                    float height = grid.tilePrefab.prefab.GetComponent<Renderer>().bounds.size.y;
                    grid.Width = width;
                    grid.Height = height;

                }
            }
        }
    }

    void OnSceneGUI()
    {
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        Event e = Event.current;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        
        Vector3 mousePos = ray.origin;
        //Is the tile going to be on screen
        bool canPlace = true;
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(mousePos);
        if (viewportPoint.x>1 || viewportPoint.x<0 || viewportPoint.y>1 || viewportPoint.y<0)
        {
            canPlace = false;
        }
        if (CheckCollision(mousePos)!=null)
        {
            canPlace = false;
        }
        //place tile
        if (e.isMouse && e.type == EventType.MouseDown && Event.current.button== 0 && canPlace)
        {
            GUIUtility.hotControl = controlID;
            e.Use();
            
            Transform prefab = grid.tilePrefab.prefab;

            GameObject gameObject;
            if (prefab)
            {
                Undo.IncrementCurrentGroup();
                Transform t = PrefabUtility.InstantiatePrefab(prefab) as Transform;
                gameObject = t.gameObject;
                Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.Width) * grid.Width + grid.Width / 2.0f, Mathf.Floor(mousePos.y / grid.Height) * grid.Height + grid.Height / 2.0f, 0.0f);
                gameObject.transform.position = aligned;
                gameObject.transform.parent = grid.transform;
                Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
            }


        }
        else if (e.isMouse && e.type == EventType.MouseDown && Event.current.button == 1)
        {
            GameObject collided = CheckCollision(mousePos);
            if (collided != null)
            {
                DestroyImmediate(collided);

            }
            GUIUtility.hotControl = controlID;
            e.Use();

        }
        //need to reselect grid since it wasn't used
        else
        {
            Selection.activeGameObject = grid.gameObject;
        }
        if(e.isMouse && e.type == EventType.MouseUp)
        {
            GUIUtility.hotControl = 0;

        }

    }
    GameObject CheckCollision(Vector3 mousePos)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll((Vector2)mousePos/* Radius */);
        if (colliders.Length >= 1) //Presuming the object you are testing also has a collider 0 otherwise
        {
            foreach (var collider in colliders)
            {
                var go = collider.gameObject; //This is the game object you collided with
                if (go == grid) continue; //Skip the object itself
                return go;
            }
        }
        return null;
    }
}
