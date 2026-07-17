using UnityEngine;

[CreateAssetMenu(fileName = "new rank", menuName = "Rank/New Rank")]
public class Rank : ScriptableObject
{
    public string rankName;
    public Sprite rankIcon;
    public int rankId;
    public float rankMultiplier;
}
