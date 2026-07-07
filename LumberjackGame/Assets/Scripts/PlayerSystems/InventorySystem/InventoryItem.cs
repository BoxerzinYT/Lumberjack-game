using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public int ID;
    public Item itemData;
    public int stackSize;

    public InventoryItem(int _id, Item item, int amount)
    {
        ID = _id;
        itemData = item;
        AddToStack(amount);
    }

    public void AddToStack(int add)
    {
        stackSize += add;
    }

    public void RemoveFromStack(int remove) 
    {
        stackSize -= remove;
    }
}
