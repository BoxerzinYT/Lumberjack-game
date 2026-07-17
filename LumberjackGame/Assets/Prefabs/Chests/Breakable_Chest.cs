using UnityEngine;

public class Breakable_Chest : BreakableObj
{
    [Header("ChestSettings")]
    [SerializeField] Chest chest;
    public Chest Chest { get { return chest; } set { chest = value; }}
    [SerializeField] SpriteRenderer downPart;
    [SerializeField] SpriteRenderer upPart;

    public void SetChest()
    {
        downPart.color = chest.chestRarity.rarityColor;
        upPart.color = chest.chestRarity.rarityColor;
        MyDropSystem.DropSettings = chest.chestDrops;
        MyDropSystem.RepeatTimes = chest.chestRepeatTimes;
    }
}
