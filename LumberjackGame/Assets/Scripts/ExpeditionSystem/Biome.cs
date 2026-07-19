using UnityEngine;

public class Biome : ScriptableObject
{
    public string biomeName;
    public Sprite biomeIcon;
    public Color biomeColor;
    public int biomeId;
    public int quantOf_toUnlockOnRandomGeneration;
    public int quantOf_toUnlockSelection;
    public int quantOf_toUnlockDifficultSelection;
}
