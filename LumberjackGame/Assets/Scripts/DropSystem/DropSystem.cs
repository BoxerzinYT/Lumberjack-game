using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] DropSettings[] dropSettings;

    public void Drop(Hec_Stats hec_stats, int phaseId)
    {
        hec_stats.Hec_Capacity += 1 + 1 * phaseId;
        float chance = Random.Range(0f,1f);
        if(chance <= 0.04 + 0.025 * phaseId)
        {
            hec_stats.MapleTree_saplings_prot += 1;
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
