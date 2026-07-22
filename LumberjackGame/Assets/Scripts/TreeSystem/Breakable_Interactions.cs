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
        //damage = _hectorStats.hec_damage;
        criticalChance = _hectorStats.All_criticalChanceMult;
        bonusChance = _hectorStats.All_bonusChanceMult;
        dropPoints = _hectorStats.All_dropPoints;
    }
}
