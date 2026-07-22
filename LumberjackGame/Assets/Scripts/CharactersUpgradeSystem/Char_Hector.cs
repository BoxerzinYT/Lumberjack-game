using UnityEngine;

[CreateAssetMenu(fileName = "New character", menuName = "Character/New character")]
public class Char_Hector : CharactersData
{
    [Header("DashPassiveSettings")]
    public float dash_distance;
    public float dash_Cooldown;
    public float dash_PowerToUsePassive;
    public float dash_OrbDuration;
    public float dash_damageUpMult;
    public float dash_damageUpDuration;
    public float dash_speedUpMult;
    public float dash_speedUpDuration;

    public float GetUpgradeValueById(int id)
    {
        if(id == 0)
        {
            return dash_distance;
        }
        else if(id == 1)
        {
            return dash_Cooldown;
        }
        else if(id == 2)
        {
            return dash_PowerToUsePassive;
        }
        else if(id == 3)
        {
            return dash_OrbDuration;
        }
        else if(id == 4)
        {
            return dash_damageUpMult;
        }
        else if(id == 5)
        {
            return dash_damageUpDuration;
        }
        else if(id == 6)
        {
            return dash_speedUpMult;
        }
        else if(id == 7)
        {
            return dash_speedUpDuration;
        }

        return 1;
    }
}
