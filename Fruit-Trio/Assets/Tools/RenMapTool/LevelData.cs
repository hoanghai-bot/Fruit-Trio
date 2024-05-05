using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObject/Create New Level Data")]

public class LevelData : ScriptableObject
{
    public List<ItemData> ListItemDataOnMaps = new List<ItemData>();
    public int FloorCount;
    public int TileCount;
    [SerializeField] private List<ItemTileData> dataFromTilemap = new List<ItemTileData> ();
}
