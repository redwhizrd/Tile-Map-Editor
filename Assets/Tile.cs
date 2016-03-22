using UnityEngine;
using System.Collections;

public class Tile : ScriptableObject {
    public enum Type
    {
        Damage,
        Gravity

    }
    public Transform prefab;

}
