using TMPro;
using Unity.Mathematics;
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
    [SerializeField] float hec_damageMult;
    public float Hec_damageMult { get { return hec_damageMult; } set
        {
            hec_damageMult = value;
            if(hec_damageMult < 1) { hec_damageMult = 1; }
        }
    }
    public Vector2 hec_rangeOffset;
    public Vector2 hec_rangeSize;
    [SerializeField] float hec_rangeMult;
    public float Hec_rangeMult { get { return hec_rangeMult; } set
        {
            hec_rangeMult = value;
            if(hec_rangeMult < 1) { hec_rangeMult = 1; }
        }
    }
    [SerializeField] float hec_criticalChance;
    public float Hec_criticalChance { get { return hec_criticalChance; } set
        {
            hec_criticalChance = value;
            if(hec_criticalChance < 1) { hec_criticalChance = 1; }
        }
    }
    [SerializeField] float hec_criticalDamageMult;
    public float Hec_criticalDamageMult { get { return hec_criticalDamageMult; } set
        {
            hec_criticalDamageMult = value;
            if(hec_criticalDamageMult < 1) { hec_criticalDamageMult = 1; }
        }
    }
    [SerializeField] float hec_bonusChance;
    public float Hec_bonusChance { get { return hec_bonusChance; } set
        {
            hec_bonusChance = value;
            if(hec_bonusChance < 1) { hec_bonusChance = 1; }
        }
    }
    public float hec_speed;
    [SerializeField] float hec_speedMult;
    public float Hec_speedMult { get { return hec_speedMult; } set
        {
            hec_speedMult = value;
            if(hec_speedMult < 1) { hec_speedMult = 1; }
        }
    }
    [SerializeField] float hec_luck;
    public float Hec_luck { get { return hec_luck; } set
        {
            hec_luck = value;
            if(hec_luck < 1) { hec_luck = 1; }
        }
    }
    [SerializeField] int hec_dropPoints;
    public int Hec_dropPoints { get { return hec_dropPoints; } set
        {
            hec_dropPoints = value;
            if(hec_dropPoints < 1) { hec_dropPoints = 1; }
        }
    }

    [SerializeField] TextMeshProUGUI coinsTxt;
    [SerializeField] Animator hec_anim;
    public Animator Hec_anim { get { return hec_anim; }}

    [SerializeField] HectorInventory hec_invent;
    public HectorInventory Hec_invent { get { return hec_invent; }}

    [SerializeField] Hec_ActionSystem hec_actSystem;
    public Hec_ActionSystem Hec_actSystem { get { return hec_actSystem; }}
    [SerializeField] AxeManager hec_axeManager;
    public AxeManager Hec_axeManager { get { return hec_axeManager; }}

    

    public void Start()
    {
        coinsTxt.text = EventsManager.eventM.UpdateVariables(hec_coins);
        hec_anim = GetComponent<Animator>();
    }

    public void AddOrRemoveMult(int addOrRemove, MultiplicatorSettings mult, float add)
    {
        switch (mult.mult.multType)
        {
            case MultType.damage:
            hec_damageMult += add * addOrRemove;
            break;
            case MultType.speed:
            hec_speedMult += add * addOrRemove;
            break;
            case MultType.criticalDamage:
            hec_criticalDamageMult += add * addOrRemove;
            break;
            case MultType.bonusChance:
            hec_bonusChance += add * addOrRemove;
            break;
            case MultType.criticalChance:
            hec_criticalChance += add * addOrRemove;
            break;
            case MultType.range:
            hec_rangeMult += add * addOrRemove;
            break;
            case MultType.luck:
            hec_luck += add * addOrRemove;
            break;
            case MultType.dropPoints:
            hec_dropPoints += (int)add * addOrRemove;
            break;
        }
    }
    public float GetAMultValue(int id)
    {
        if(id == 0)
        {
            return hec_damageMult;
        }
        else if(id == 1)
        {
            return hec_speedMult;
        }
        else if(id == 2)
        {
            return hec_criticalDamageMult;
        }
        else if(id == 3)
        {
            return hec_bonusChance;
        }
        else if(id == 4)
        {
            return hec_criticalChance;
        }
        else if(id == 5)
        {
            return hec_rangeMult;
        }
        else if(id == 6)
        {
            return hec_luck;
        }
        else if(id == 7)
        {
            return hec_dropPoints;
        }

        return (id != 7) ? 1 : 0;
    }
}
[System.Serializable]
public sealed class MultiplicatorSettings
{
    public string multName;
    public Multiplicator mult;
    public float add;
    public float duration;
}
