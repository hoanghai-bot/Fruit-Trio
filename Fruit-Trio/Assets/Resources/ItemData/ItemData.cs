using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE
{
    ITEM_1 = 1,
    ITEM_2 = 2,
    ITEM_3 = 3,
    ITEM_4 = 4,
    ITEM_5 = 5,
    ITEM_6 = 6,
    ITEM_7 = 7,
    ITEM_8 = 8,
    ITEM_9 = 9,
    ITEM_10 = 10,
    ITEM_11 = 11,
    ITEM_12 = 12,
    ITEM_13 = 13,
    ITEM_14 = 14,
    ITEM_15 = 15,
    ITEM_16 = 16,
    ITEM_17 = 17,
    ITEM_18 = 18,
    ITEM_19 = 19,
    ITEM_20 = 20
}
[CreateAssetMenu(fileName ="ItemData",menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public ITEM_TYPE itemType;
}
