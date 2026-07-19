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
        criticalChance = _hectorStats.Hec_criticalChance;
        bonusChance = _hectorStats.Hec_bonusChance;
        dropPoints = _hectorStats.Hec_dropPoints;
    }
}
