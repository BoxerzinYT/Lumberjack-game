using UnityEngine;

[CreateAssetMenu(fileName = "new rarity", menuName = "Rarity/Rarity")]

public class ItemRarity : ScriptableObject
{
    public string rarityName;
    [Tooltip("0=Common, 1=Rare, 2=Mega, 3=Epic, 4=Legend, 5=Galaxy")]
    public int rarityId;
    public Color rarityColor;
}
