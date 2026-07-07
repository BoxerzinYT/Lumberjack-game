using UnityEngine;

[CreateAssetMenu(fileName = "new sellItem", menuName = "Item/SellItem")]
public class Sell_Item : Item
{
    [Header("Sell settings")]
    public float itemValue;
    public float[] itemSellXp;
}
