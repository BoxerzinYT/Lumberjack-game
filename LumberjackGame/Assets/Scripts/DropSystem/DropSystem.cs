using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] Sell_Item mapleSappling;
    [SerializeField] DropSettings[] dropSettings;

    public void Drop(Hec_Stats hec_stats, int phaseId)
    {
        float chance = Random.Range(0f,1f);
        foreach(var d in dropSettings[phaseId].myDrops)
        {
            if(chance <= d.chance)
            {
                hec_stats.Hec_invent.hectorInventory.AddItem(1, new InventoryItem(hec_stats.Hec_invent.hectorInventory.GetIDOfAItem(d.item), d.item, 1));
            }
        }
    }
}
[System.Serializable]
public sealed class DropSettings
{
    public string settingName;
    public Drop[] myDrops;
}
[System.Serializable]
public sealed class Drop
{
    public Item item;
    public float chance;
}
