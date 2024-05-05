using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTileConnect : MonoBehaviour
{
    public ItemTile itemTile;
    public int countFront => listTileFrontof.Count;

    public List<ItemTile> listTileFrontof = new List<ItemTile>();
    public List<ItemTile> listTileBehind = new List<ItemTile>();
    // Start is called before the first frame update
    void Start()
    {
        itemTile = GetComponent<ItemTile>();
    }

    
    public void DoWhenMoveToSlot()
    {
        CheckListBehind();
        CheckListFront();
    }

    void CheckListBehind()
    {
        if(listTileBehind is null) return; 
        foreach(var tile in listTileBehind)
        {
            tile.tileConnect.RemoveTileBefore(this.itemTile);
            tile.tileConnect.CheckToUnlock();
        }
    }
    void CheckListFront()
    {
        if (listTileFrontof is null) { return; }
        foreach (var tile in listTileFrontof)
        {
            tile.tileConnect.RemoveTileBehind(this.itemTile);
        }
    }
    public void CheckToUnlock()
    {
        if (countFront == 0)
        {
            itemTile.SetTouch_Avaiable(true);
            itemTile.SetShadow_Avaiable(false);
        }
    }
    public void RemoveTileBefore(ItemTile itemTile)
    {
        if (listTileFrontof.Contains(itemTile))
        {
            listTileFrontof.Remove(itemTile);
        }
    }
    public void RemoveTileBehind(ItemTile itemTile)
    {
        if (listTileBehind.Contains(itemTile))
        {
            listTileBehind.Remove(itemTile);
        }
    }
    public void DoWhenUndo()
    {
        listTileFrontof = new List<ItemTile>();
        listTileBehind = new List<ItemTile>();
        //SetTileSelf
        listTileFrontof = LevelManager.instance.GetTileFloorUper(this.itemTile);
        listTileBehind = LevelManager.instance.GetTileFloorDown(this.itemTile);
        //SetOtherTile
        if (listTileFrontof is not null)
        {
            foreach (var tile in listTileFrontof)
            {
                tile.tileConnect.listTileBehind.Add(this.itemTile);
            }
        }

        if (listTileBehind is not null)
        {
            foreach (var tile in listTileBehind)
            {
                tile.tileConnect.listTileFrontof.Add(this.itemTile);
                tile.SetTouch_Avaiable(false);
                tile.SetShadow_Avaiable(true);
            }
        }
    }

}