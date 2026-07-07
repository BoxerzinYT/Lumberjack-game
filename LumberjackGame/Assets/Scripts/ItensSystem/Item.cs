using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDescription;
    public ItemRarity itemRarity;
    public Sprite itemIcon;

    public bool isStackable;
}
