using System;

[Serializable]
public class InventoryItem
{
    public int ID;
    public Item itemData;
    public float stackSize;

    public InventoryItem(int _id, Item item, float amount)
    {
        ID = _id;
        itemData = item;
        AddToStack(amount);
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
