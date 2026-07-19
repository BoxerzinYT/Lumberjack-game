using UnityEngine;

public class Chest : ScriptableObject
{
    public string chestName;
    public ItemRarity chestRarity;
    public DropSettings[] chestDrops;
    public int chestRepeatTimes;
}
