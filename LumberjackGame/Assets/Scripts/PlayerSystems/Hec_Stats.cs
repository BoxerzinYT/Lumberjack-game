using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Hec_Stats : MonoBehaviour
{
    [SerializeField] Characters_LevelManager levelMan;
    public Characters_LevelManager LevelMan { get { return levelMan; }}
    [Header("CoinsStats")]
    [SerializeField] float cha_coins;
    public float Cha_coins { get { return cha_coins; } set
        {
            cha_coins = value;
            if(cha_coins < 0)
            {
                cha_coins = 0;
            }

            coinsTxt.text = EventsManager.eventM.UpdateVariables(cha_coins);
        }
    }

    public CharactersData[] charactersData;
    public CharactersBaseStats characterSelectedStatus;
    public int characterSelectedId;
    [SerializeField] UnityEvent whenLoad;

    [Header("AttributeStats")]
    [SerializeField] float all_damageMult;
    public float All_damageMult { get { return all_damageMult; } set
        {
            all_damageMult = value;
            if(all_damageMult < 1) { all_damageMult = 1; }
        }
    }
    [SerializeField] float all_speedMult;
    public float All_speedMult { get { return all_speedMult; } set
        {
            all_speedMult = value;
            if(all_speedMult < 1) { all_speedMult = 1; }
        }
    }
    [SerializeField] float all_criticalDamageMult;
    public float All_criticalDamageMult { get { return all_criticalDamageMult; } set
        {
            all_criticalDamageMult = value;
            if(all_criticalDamageMult < 1) { all_criticalDamageMult = 1; }
        }
    }
    [SerializeField] float all_bonusChanceMult;
    public float All_bonusChanceMult { get { return all_bonusChanceMult; } set
        {
            all_bonusChanceMult = value;
            if(all_bonusChanceMult < 1) { all_bonusChanceMult = 1; }
        }
    }
    [SerializeField] float all_criticalChanceMult;
    public float All_criticalChanceMult { get { return all_criticalChanceMult; } set
        {
            all_criticalChanceMult = value;
            if(all_criticalChanceMult < 1) { all_criticalChanceMult = 1; }
        }
    }
    [SerializeField] float all_rangeMult;
    public float All_rangeMult { get { return all_rangeMult; } set
        {
            all_rangeMult = value;
            if(all_rangeMult < 1) { all_rangeMult = 1; }
        }
    }
    [SerializeField] float all_luckMult;
    public float All_luckMult { get { return all_luckMult; } set
        {
            all_luckMult = value;
            if(all_luckMult < 1) { all_luckMult = 1; }
        }
    }
    [SerializeField] int all_dropPoints;
    public int All_dropPoints { get { return all_dropPoints; } set
        {
            all_dropPoints = value;
            if(all_dropPoints < 1) { all_dropPoints = 1; }
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
        coinsTxt.text = EventsManager.eventM.UpdateVariables(cha_coins);
        hec_anim = GetComponent<Animator>();

        LoadSelectedCharacterStatus();
    }

    public void LoadSelectedCharacterStatus()
    {
        characterSelectedStatus.characterName = charactersData[characterSelectedId].characterName;
        characterSelectedStatus.characterDescription = charactersData[characterSelectedId].characterDescription;
        characterSelectedStatus.characterIcon = charactersData[characterSelectedId].characterIcon;
        characterSelectedStatus.characterType = charactersData[characterSelectedId].characterType;

        characterSelectedStatus.cha_damage = charactersData[characterSelectedId].cha_damage + charactersData[0].incrementsPerLevel[0] * levelMan.GetCharactersLevel(0);
        characterSelectedStatus.cha_speed = charactersData[characterSelectedId].cha_speed + charactersData[0].incrementsPerLevel[1] * levelMan.GetCharactersLevel(0);
        characterSelectedStatus.cha_criticalDamageMult = charactersData[characterSelectedId].cha_criticalDamageMult + charactersData[0].incrementsPerLevel[2] * levelMan.GetCharactersLevel(0);
        characterSelectedStatus.cha_criticalChance = charactersData[characterSelectedId].cha_criticalChance + charactersData[0].incrementsPerLevel[3] * levelMan.GetCharactersLevel(0);
        characterSelectedStatus.cha_bonusChance = charactersData[characterSelectedId].cha_bonusChance + charactersData[0].incrementsPerLevel[4] * levelMan.GetCharactersLevel(0);

        characterSelectedStatus.cha_rangeOffset = charactersData[characterSelectedId].cha_rangeOffset;
        characterSelectedStatus.cha_rangeSize = charactersData[characterSelectedId].cha_rangeSize;
        characterSelectedStatus.cha_rangeSizeMult = charactersData[characterSelectedId].cha_rangeSizeMult + charactersData[0].incrementsPerLevel[5] * levelMan.GetCharactersLevel(0);

        
        characterSelectedStatus.cha_luckMult = charactersData[characterSelectedId].cha_luckMult + charactersData[0].incrementsPerLevel[6] * levelMan.GetCharactersLevel(0);
        characterSelectedStatus.cha_dropPoints = charactersData[characterSelectedId].cha_dropPoints + (int)charactersData[0].incrementsPerLevel[7] * levelMan.GetCharactersLevel(0);

        whenLoad?.Invoke();
    }

    public void AddOrRemoveGeneralMult(int addOrRemove, MultiplicatorSettings mult, float add)
    {
        switch (mult.mult.multType)
        {
            case MultType.damage:
            all_damageMult += add * addOrRemove;
            break;
            case MultType.speed:
            all_speedMult += add * addOrRemove;
            break;
            case MultType.criticalDamage:
            all_criticalDamageMult += add * addOrRemove;
            break;
            case MultType.bonusChance:
            all_bonusChanceMult += add * addOrRemove;
            break;
            case MultType.criticalChance:
            all_criticalChanceMult += add * addOrRemove;
            break;
            case MultType.range:
            all_rangeMult += add * addOrRemove;
            break;
            case MultType.luck:
            all_luckMult += add * addOrRemove;
            break;
            case MultType.dropPoints:
            all_dropPoints += (int)add * addOrRemove;
            break;
        }
    }
    public void AddOrRemoveCharacterMult(int addOrRemove, int charId, MultiplicatorSettings mult, float add)
    {
        switch (mult.mult.multType)
        {
            case MultType.damage:
            characterSelectedStatus.cha_damage += add * addOrRemove;
            break;
            case MultType.speed:
            characterSelectedStatus.cha_speed += add * addOrRemove;
            break;
            case MultType.criticalDamage:
            characterSelectedStatus.cha_criticalDamageMult += add * addOrRemove;
            break;
            case MultType.bonusChance:
            characterSelectedStatus.cha_bonusChance += add * addOrRemove;
            break;
            case MultType.criticalChance:
            characterSelectedStatus.cha_criticalChance += add * addOrRemove;
            break;
            case MultType.range:
            characterSelectedStatus.cha_rangeSizeMult += add * addOrRemove;
            break;
            case MultType.luck:
            characterSelectedStatus.cha_luckMult += add * addOrRemove;
            break;
            case MultType.dropPoints:
            characterSelectedStatus.cha_dropPoints += (int)add * addOrRemove;
            break;
        }
    }
    public float GetAGeneralMultValue(int id)
    {
        if(id == 0)
        {
            return all_damageMult;
        }
        else if(id == 1)
        {
            return all_speedMult;
        }
        else if(id == 2)
        {
            return all_criticalDamageMult;
        }
        else if(id == 3)
        {
            return all_bonusChanceMult;
        }
        else if(id == 4)
        {
            return all_criticalChanceMult;
        }
        else if(id == 5)
        {
            return all_rangeMult;
        }
        else if(id == 6)
        {
            return all_luckMult;
        }
        else if(id == 7)
        {
            return all_dropPoints;
        }

        return (id != 7) ? 1 : 0;
    }
    public float GetACharacterMultValue(int id, int charId)
    {
        if(id == 0)
        {
            return characterSelectedStatus.cha_damage;
        }
        else if(id == 1)
        {
            return characterSelectedStatus.cha_speed ;
        }
        else if(id == 2)
        {
            return characterSelectedStatus.cha_criticalDamageMult;
        }
        else if(id == 3)
        {
            return characterSelectedStatus.cha_bonusChance;
        }
        else if(id == 4)
        {
            return characterSelectedStatus.cha_criticalChance;
        }
        else if(id == 5)
        {
            return characterSelectedStatus.cha_rangeSizeMult;
        }
        else if(id == 6)
        {
            return characterSelectedStatus.cha_luckMult;
        }
        else if(id == 7)
        {
            return characterSelectedStatus.cha_dropPoints;
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
[System.Serializable]
public sealed class CharactersBaseStats
{
    public string characterName;
    public Sprite characterIcon;
    [TextArea]
    public string characterDescription;
    public CharacterType characterType;
    public float cha_damage;
    public float cha_speed;
    public float cha_criticalDamageMult;
    public float cha_bonusChance;
    public float cha_criticalChance;
    public Vector2 cha_rangeOffset;
    public Vector2 cha_rangeSize;
    public float cha_rangeSizeMult;
    public float cha_luckMult;
    public int cha_dropPoints;
}
