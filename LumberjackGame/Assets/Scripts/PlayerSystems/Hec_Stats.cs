using UnityEngine;

public class Hec_Stats : MonoBehaviour
{
    [Header("CoinsStats")]
    public float hec_coins;
    public int hec_level;
    public float hec_xp;
    public float hec_xpForNextLevel;

    [Header("AttributeStats")]
    public float hec_damage;
    public float hec_range;
    public float hec_criticalChance;
    public float hec_bonusChance;
    public float hec_speed;
    public float hec_luck;
    public int hec_dropPoints;

    [SerializeField] HectorInventory hec_invent;
    public HectorInventory Hec_invent { get { return hec_invent; }}

    [SerializeField] Hec_ActionSystem hec_actSystem;
    public Hec_ActionSystem Hec_actSystem { get { return hec_actSystem; }}
}
