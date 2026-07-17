using UnityEngine;

[CreateAssetMenu(fileName = "new difficult", menuName = "Difficults/Difficult")]
public class Difficult : ScriptableObject
{
    public string difficultName;
    public int difficultId;

    public Sprite difficultIcon;
    public Color difficultColor;
}
