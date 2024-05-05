using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotStat
{
    Normal,
    WaitToDesTroy,
    WaitToSort
}
public class ItemTileSlot : MonoBehaviour
{
    public SlotStat slotStat = SlotStat.Normal;
    public ItemTile itemTile;
    public bool tileIsNull => itemTile is null;
    int index => LevelManager.instance.slotHolder.listItemSlots.IndexOf(this);
    public GameObject BG;

    public void InitSlot(ItemTile _itemTile)
    {
        _itemTile.slot = this;
        this.itemTile = _itemTile;
        _itemTile.transform.parent = this.transform;
        _itemTile.itemTileState = ITEMTILE_STATE.SLOT;
    }
    public void SetCenterPos()
    {
        SetLayerInSlot(itemTile);
        SetOrderInSlot(itemTile);
        itemTile.transform.DOMove(this.transform.position, 0.25f);
    }


    public void ClearSlot()
    {
        itemTile = null;
        slotStat = SlotStat.Normal;
    }
    public void SetLayerInSlot(ItemTile _itemTile)
    {
        _itemTile.bg.sortingLayerName = "Slot";
        _itemTile.icon.sortingLayerName = "Slot";
        _itemTile.shadow.sortingLayerName = "Slot";
    }
    public void SetOrderInSlot(ItemTile _itemTile)
    {
        int sortingOrder = index;
        _itemTile.bg.sortingOrder = sortingOrder;
        _itemTile.icon.sortingOrder = sortingOrder;
        _itemTile.shadow.sortingOrder = sortingOrder;
    }
}
