using UnityEngine;

[CreateAssetMenu(fileName = "new sappling", menuName = "Sappling/New sappling")]
public class Sappling : ActionItem
{
    [Header("SapplingSettings")]
    public Breakable_Tree myTree;
    public Breakable_Tree myExpeditionTree;
}
