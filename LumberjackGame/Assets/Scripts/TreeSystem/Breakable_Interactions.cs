using UnityEngine;

public class Breakable_Interactions : MonoBehaviour
{
    public float damage { get; private set;}
    public float criticalChance { get; private set;}
    public float bonusChance { get; private set;}
    public int dropPoints { get; private set;}

    public Breakable_Interactions
    (Hec_Stats _hectorStats)
    {
        damage = _hectorStats.hec_damage;
        criticalChance = _hectorStats.hec_criticalChance;
        bonusChance = _hectorStats.hec_bonusChance;
        dropPoints = _hectorStats.hec_dropPoints;
    }
}
