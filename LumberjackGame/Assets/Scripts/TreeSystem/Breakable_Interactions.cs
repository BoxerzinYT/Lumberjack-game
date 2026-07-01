using UnityEngine;

public class Breakable_Interactions : MonoBehaviour
{
    public float damage { get; private set;}
    public float criticalChance { get; private set;}
    public float bonusChance { get; private set;}
    public int dropPoints { get; private set;}

    public Breakable_Interactions
    (float _damage, float _criticalChance, float _bonusChance, int _dropPoints)
    {
        damage = _damage;
        criticalChance = _criticalChance;
        bonusChance = _bonusChance;
        dropPoints = _dropPoints;
    }
}
