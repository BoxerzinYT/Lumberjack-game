using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDescription;
    public ItemRarity itemRarity;
    public Biome itemBiome;
    public Sprite itemIcon;

    public bool isStackable = true;
    public bool canChangeMyRank = true;
}
