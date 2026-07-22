using UnityEngine;

public class CharactersData : ScriptableObject
{
    [Header("MainStats")]
    public string characterName;
    public Sprite characterIcon;
    [TextArea]
    public string characterDescription;
    public CharacterType characterType;

    [Header("GeneralStats")]
    public float cha_damage;
    public float cha_speed;
    public float cha_criticalDamageMult;
    public float cha_bonusChance;
    public float cha_criticalChance;
    public Vector2 cha_rangeOffset;
    public Vector2 cha_rangeSize;
    public float cha_rangeSizeMult;
    public float cha_luckMult;
    public int cha_dropPoints;

    [Header("UpgradeSettings")]
    public float firstPrice;
    public float priceMult;
    public float[] incrementsPerLevel;
    public float[] passiveIncrementsPerLevel;
    public int levelCap;
}
[System.Serializable]
public enum CharacterType
{
    Damage,
    Support,
}
