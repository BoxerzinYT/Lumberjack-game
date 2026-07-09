using TMPro;
using UnityEngine;

public class Hec_Stats : MonoBehaviour
{
    [Header("CoinsStats")]
    [SerializeField] float hec_coins;
    public float Hec_coins { get { return hec_coins; } set
        {
            hec_coins = value;
            if(hec_coins < 0)
            {
                hec_coins = 0;
            }

            coinsTxt.text = EventsManager.eventM.UpdateVariables(hec_coins);
        }
    }
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

    [SerializeField] TextMeshProUGUI coinsTxt;

    [SerializeField] HectorInventory hec_invent;
    public HectorInventory Hec_invent { get { return hec_invent; }}

    [SerializeField] Hec_ActionSystem hec_actSystem;
    public Hec_ActionSystem Hec_actSystem { get { return hec_actSystem; }}

    public void Start()
    {
        coinsTxt.text = EventsManager.eventM.UpdateVariables(hec_coins);
    }
}
