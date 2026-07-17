using System;

[Serializable]
public class InventoryItem
{
    public int ID;
    public Item itemData;
    public Rank itemRank;
    public float stackSize;

    public InventoryItem(Item _itemData, Rank _itemRank, float _stack)
    {
        itemData = _itemData;
        itemRank = _itemRank;
        AddToStack(_stack);
    }

    public void AddToStack(float add)
    {
        stackSize += add;
    }

    public void RemoveFromStack(float remove) 
    {
        stackSize -= remove;
    }
}
