using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotHolder : MonoBehaviour
{
    public ItemTileSlot slotPrefab;
    public List<ItemTileSlot> listItemSlots = new List<ItemTileSlot>();
    public int slotMax = 7;
    public void SpawnSlot()
    {
        listItemSlots.Clear();
        for (int i = 0; i < slotMax; i++)
        {
            ItemTileSlot newSlot = Instantiate(slotPrefab, transform);
            newSlot.transform.localPosition = new Vector3(-5.1f + 1.05f * i, 0.1f, 0);
            newSlot.transform.localScale = Vector3.one * 1 / 1.2f;
            listItemSlots.Add(newSlot);
        }
    }
    public bool CheckLoss()
    {

        return CountTileNormal() >= 7;
    }
    public int CountTileNormal()
    {
        int results = 0;
        foreach (var slot in listItemSlots)
        {
            if (!slot.tileIsNull && slot.slotStat.Equals(SlotStat.Normal))
            {
                results++;
            }
        }
        return results;
    }
    public void Clear()
    {
        foreach (var x in listItemSlots)
        {
            Destroy(x.gameObject);
        }
    }
    public void AddItemSlot(ItemTile itemTile)
    {
        if(CountSlotNotNull() == listItemSlots.Count)
        {
            SpawnMoreSlot();
        }
        LevelManager.instance.listItemTiles.Remove(itemTile);
        int indexNewSlot = FindIndexToAdd(itemTile);
        LevelManager.instance.listUndo.Add(itemTile);
        MoveItemToSlot(itemTile, indexNewSlot);
    }
    void MoveItemToSlot(ItemTile item, int index)
    {
        ChuanbiNhanTile(index);
        listItemSlots[index].InitSlot(item);
        listItemSlots[index].SetCenterPos();
        CheckSlotMatch3(item);
    }
    void ChuanbiNhanTile(int index)
    {
        if (index == listItemSlots.Count - 1) { return; }
        for(int i = listItemSlots.Count -1; i > index;i--)
        {
            if (listItemSlots[i - 1].tileIsNull) continue;
            MoveSlot(listItemSlots[i - 1], listItemSlots[i]);
        }
    }
    public void MoveSlot(ItemTileSlot from, ItemTileSlot to)
    {
        to.InitSlot(from.itemTile);
        to.SetCenterPos();
        to.slotStat = from.slotStat;
        from.ClearSlot();
    }
    public int CountSlotNotNull()
    {
        int results = 0;
        foreach (var slot in listItemSlots)
        {
            if(!slot.tileIsNull) results++;

        }
        return results;
    }
    public void SpawnMoreSlot()
    {
        ItemTileSlot slot = Instantiate(slotPrefab, this.transform);
        slot.transform.localPosition = new Vector3(-5.1f + 1.05f * listItemSlots.Count, 0.1f, 0);
        listItemSlots.Add(slot);
        slot.BG.SetActive(false);
    }
    public int FindIndexToAdd(ItemTile item)
    {
        int result = 0;
        int indexSlot = listItemSlots.Count - 1;
        for (int i = listItemSlots.Count - 1; i >= 0; i--)
        {
            if (listItemSlots[i].tileIsNull) { result = i; continue; }
            if (listItemSlots[i].itemTile.itemData.itemType == item.itemData.itemType)
            {
                return i + 1;
            }
        }
        indexSlot = result;
        return indexSlot;
    }
    public void CheckSlotMatch3(ItemTile _itemTile)
    {
        List<ItemTileSlot> listCheckMatch3Slots = FindMatch3Item(_itemTile);
        if (listCheckMatch3Slots.Count == 3)
        {
            List<ItemTile> destroyList = new List<ItemTile>();
            for (int i = 0; i < 3; i++)
            {
                LevelManager.instance.listUndo.Remove(listCheckMatch3Slots[i].itemTile);
                destroyList.Add(listCheckMatch3Slots[i].itemTile);
                listCheckMatch3Slots[i].slotStat = SlotStat.WaitToDesTroy;
                Sequence mySequence = DOTween.Sequence();
                mySequence
                 .Append(listCheckMatch3Slots[i].itemTile.transform.DOScale(Vector3.one * 1.5f, 0.125f).SetEase(Ease.Linear).SetDelay(0.5f))
                 .Append(listCheckMatch3Slots[i].itemTile.transform.DOScale(Vector3.one * 0.25f, 0.125f).SetEase(Ease.Linear))
                 .AppendInterval(GamePlayManager.instance.point += 100);
                GamePlayManager.instance.ShowPoint();
            }

            FunctionCommon.DelayTime(0.75f, () =>
            {
                foreach (var item in destroyList)
                {
                    item.SetMatch3();
                }
                SortData();
            });
            if (LevelManager.instance.CheckWin())
            {
                StartCoroutine(LevelManager.instance.DoNextLevel());
                return;
            }


        }
        else
        {
            if (CheckLoss())
            {
                //LevelManager.instance.TryAgain();
                GamePlayManager.instance.PopUpLose.SetActive(true);
                LevelManager.instance.levelStat = LevelStat.RESTART;
            }
        }



    }
    public List<ItemTileSlot> FindMatch3Item(ItemTile _itemTile)
    {
        List<ItemTileSlot> listCheckMatch3_ItemTileSlot = new List<ItemTileSlot>();
        for (int i = 0; i < listItemSlots.Count; i++)
        {
            if (listItemSlots[i].tileIsNull || listItemSlots[i].slotStat == SlotStat.WaitToDesTroy) continue;
            if (listItemSlots[i].itemTile.itemData.itemType == _itemTile.itemData.itemType)
            {
                listCheckMatch3_ItemTileSlot.Add(listItemSlots[i]);
                if (listCheckMatch3_ItemTileSlot.Count == 3)
                {
                    return listCheckMatch3_ItemTileSlot;
                }
            }

        }
        return listCheckMatch3_ItemTileSlot;
    }
    public void SortData()
    {
        for (int i = 0; i < listItemSlots.Count; i++)
        {
            if (!listItemSlots[i].tileIsNull) continue;
            for (int j = i; j < listItemSlots.Count; j++)
            {
                if (!listItemSlots[j].tileIsNull)
                {
                    MoveSlot(listItemSlots[j], listItemSlots[i]);
                    break;

                }
            }
        }
    }
}
