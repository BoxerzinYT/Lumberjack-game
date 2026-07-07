using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] Sell_Item mapleSappling;
    [SerializeField] DropSettings[] dropSettings;

    public void Drop(Hec_Stats hec_stats, int phaseId)
    {
        float chance = Random.Range(0f,1f);
        if(chance <= 0.6 + 0.025 * phaseId)
        {
            hec_stats.Hec_invent.hectorInventory.AddItem(1, new InventoryItem(0, mapleSappling, 1));
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
    public float chance;
}
