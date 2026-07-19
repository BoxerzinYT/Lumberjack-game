using UnityEngine;

[CreateAssetMenu(fileName = "new axe part", menuName = "Axe/Part")]
public class AxePart : Item
{
    [Header("AxePartSettings")]
    public Sprite partSprite;
    public AxePartType partType;
    public MultiplicatorSettings[] mults;

}
[System.Serializable]
public enum AxePartType
{
    body,
    head
};
