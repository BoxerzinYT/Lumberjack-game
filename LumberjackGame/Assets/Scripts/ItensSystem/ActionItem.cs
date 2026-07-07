using UnityEngine;

[CreateAssetMenu(fileName = "new action item", menuName = "Item/ActionItem")]
public class ActionItem : Sell_Item
{
    [Header("ActionSettings")]
    public TypeOfAction typeOfAction;
    [Tooltip("0=Maple, 1=Forest")] //É o id dos biomas do jogo, exemplo: 0 representa maple biome, 1 representa forest biome...
    public int actionId;
}
[System.Serializable]
public enum TypeOfAction
{
    sappling,
    expansion
};
