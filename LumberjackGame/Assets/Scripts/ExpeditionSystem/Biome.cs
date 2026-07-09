using UnityEngine;

[CreateAssetMenu(fileName = "new biome", menuName = "Biome/new biome")]
public class Biome : ScriptableObject
{
    public string biomeName;
    public Color biomeColor;
    public int quantOf_toUnlockOnRandomGeneration;
    public int quantOf_toUnlockSelection;
    public int quantOf_toUnlockDifficultSelection;
}
