using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ITEMTILE_STATE
{
    START,
    START_TO_FLOOR,
    FLOOR,
    HOVER,
    MOVE_TO_SLOT,
    SLOT
}
public class ItemTile : MonoBehaviour
{
    public static float TILE_SIZE = 1f;

    public SpriteRenderer bg;
    public SpriteRenderer icon;
    public SpriteRenderer shadow;

    public ItemTileConnect tileConnect;
    public Collider2D touchCollider2D;
    //============POS
    public int indexOnMap;
    public int floorIndex;
    public Vector2Int posTile;
    public Vector3 localPos => new Vector3(TILE_SIZE * posTile.x, TILE_SIZE * posTile.y, 50 - floorIndex * 5);
    //============DATA

    public ItemData itemData;
    public ITEMTILE_STATE itemTileState = ITEMTILE_STATE.START;
    //============INGAME
    bool isMouseDown = false;
    public ItemTileSlot slot;

    public void InitTile(int _indexOnMap, int _floorIndex, Vector2Int _posTile, ItemData _itemData)
    {
        indexOnMap = _indexOnMap;
        floorIndex = _floorIndex;
        itemData = _itemData;
        posTile = _posTile;
        icon.sprite = LevelManager.instance.fruits[(int)itemData.itemType - 1];

        gameObject.transform.localPosition = new Vector3(TILE_SIZE * posTile.x, TILE_SIZE * posTile.y, 50 - floorIndex * 5);
        SetScaleFloor();
        SetOrderLayer_Floor();
        SetLayer_Floor();
    }
    public void SetMatch3()
    {
        slot.slotStat = SlotStat.Normal;
        slot.itemTile = null;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 1f);
    }
    private void OnMouseDown()
    {
        if (LevelManager.instance.levelStat == LevelStat.RESTART) return;
        isMouseDown = true;
    }
    private void OnMouseUp()
    {
        if (LevelManager.instance.levelStat == LevelStat.RESTART) return;
        if (isMouseDown)
        {
            isMouseDown = false;
            SetTouchItemTile();
        }
        if (bg.sortingLayerName.Equals("Hover"))
        {
            SetLayer_Floor();
        }
    }
    public void OnMouseEnter()
    {

    }
    public void OnMouseExit()
    {

    }
    public void SetUndo()
    {
        this.transform.parent = LevelManager.instance.listFloors[this.floorIndex];
        this.transform.DOLocalMove(new Vector3(ItemTile.TILE_SIZE * this.posTile.x, ItemTile.TILE_SIZE * this.posTile.y, 50 - this.floorIndex * 5), 0.25f);
        SetTouch_Avaiable(true);
        tileConnect.DoWhenUndo();

        itemTileState = ITEMTILE_STATE.FLOOR;
        SetScaleFloor();
        SetOrderLayer_Floor();
        SetLayer_Floor();

    }

    public void SetTouchItemTile()
    {
        if (itemTileState == ITEMTILE_STATE.FLOOR)
        {
            SetLayer_Move();
            SetTouch_Avaiable(false);
            SetShadow_Avaiable(false);
            tileConnect.DoWhenMoveToSlot();   
            LevelManager.instance.slotHolder.AddItemSlot(this);
        }
    }

    public void SetScaleFloor() 
    {
        gameObject.transform.localScale = new Vector3(TILE_SIZE / 1.2f, TILE_SIZE / 1.2f, TILE_SIZE / 1.2f); 
    }
    public void SetOrderLayer_Floor()
    {
        int sortingOrder = 400 - 20 * posTile.y + posTile.x;
        bg.sortingOrder = sortingOrder;
        icon.sortingOrder = sortingOrder;
        shadow.sortingOrder = sortingOrder;
    }
    public void SetLayer_Floor()
    {
        isMouseDown = false;
        string sortingLayerName = "Floor" + (floorIndex + 1);

        bg.sortingLayerName = sortingLayerName;
        icon.sortingLayerName = sortingLayerName;
        shadow.sortingLayerName = sortingLayerName;
    }
    public void SetLayer_Move()
    {
        bg.sortingLayerName = "Move";
        icon.sortingLayerName = "Move";
        shadow.sortingLayerName = "Move";
    }
    public void SetShadow_Avaiable(bool isShadowAvaiable)
    {
        shadow.gameObject.SetActive(isShadowAvaiable);
    }
    public void SetTouch_Avaiable(bool _isTouchAvaiable)
    {
        touchCollider2D.enabled = _isTouchAvaiable;
    }



}
