using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] DropUIManager dropPrefab;
    [SerializeField] DropSettings[] dropSettings;
    public DropSettings[] DropSettings { get { return dropSettings; } set { dropSettings = value; }}
    [SerializeField] int repeatTimes;
    public int RepeatTimes { get { return repeatTimes; } set { repeatTimes = value; }}

    public void Drop(Hec_Stats hec_stats, int phaseId, float rankMult)
    {
        int repeatTimesFromThePoints = hec_stats.Hec_dropPoints / 100;
        float chanceForOther = ((float)hec_stats.Hec_dropPoints / 100) - repeatTimesFromThePoints;
        float chanceForOtherRepeatPoint = Random.Range(0f,1f);
        if(chanceForOtherRepeatPoint <= chanceForOther)
        {
            repeatTimesFromThePoints++;
        }

        for(int i=0; i<repeatTimes + repeatTimesFromThePoints; i++)
        {
            float dropChance = Random.Range(0f,1f);
            foreach(var d in dropSettings[phaseId].myDrops)
            {
                if(EventsManager.eventM.CalculateAChance(dropChance * rankMult, d.chance))
                {
                    float quantDropChance = Random.Range(0f,1f);
                    foreach(var qd in d.quants)
                    {
                        if(EventsManager.eventM.CalculateAChance(quantDropChance * rankMult, qd.chance))
                        {
                            DropUIManager newDrop = Instantiate(dropPrefab);
                            newDrop.transform.position = transform.position;
                            newDrop.SetDrop(d.item.itemIcon, EventsManager.eventM.UpdateVariables(qd.quant));
                            hec_stats.Hec_invent.hectorInventory.
                            AddItem(qd.quant, new InventoryItem
                            (d.item, d.itemRank, 1));
                        }
                    }
                }
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
    public Rank itemRank;
    public float chance;
    public DropQuantChanceSettings[] quants;
}
[System.Serializable]
public sealed class DropQuantChanceSettings
{
    public int quant = 1;
    public float chance = 1;
}
