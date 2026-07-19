using UnityEngine;

public class Multiplicator : ScriptableObject
{
    public string multName;
    public int multId;
    public Sprite multIcon;
    public MultType multType;
}
[System.Serializable]
public enum MultType
{
    damage,
    speed,
    criticalDamage,
    bonusChance,
    criticalChance,
    range,
    luck,
    dropPoints,
};
