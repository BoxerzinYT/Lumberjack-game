using TMPro;
using UnityEngine;

public class Struc_SettingsPanel : MonoBehaviour
{
    [SerializeField] Hec_Stats hecStats;
    [SerializeField] TextMeshProUGUI dropPointsTxt;

    public void UpdateDropPointsTxt()
    {
        int repeatTimesFromThePoints = hecStats.All_dropPoints / 100;
        float chanceForOther = ((float)hecStats.All_dropPoints / 100) - repeatTimesFromThePoints;
        dropPointsTxt.text = "Your drop points: " + repeatTimesFromThePoints;
        if(chanceForOther != 0)
        {
            dropPointsTxt.text += " + " + chanceForOther * 100 + "%" + " for +1";
        }
    }
}
