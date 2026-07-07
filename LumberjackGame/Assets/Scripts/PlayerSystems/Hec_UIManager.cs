using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hec_UIManager : MonoBehaviour
{
    Hec_Stats myStats;
    UI_UpdateBars myUpdateBarScp;
    UI_ChangeAText myChangeTxtScp;
    [SerializeField] Image hec_capacityBar;
    [SerializeField] TextMeshProUGUI hec_capacityTxt;
    [SerializeField] Gradient capacityBarGradient;

    [Header("Prototype")]
    [SerializeField] TextMeshProUGUI saplingsTxt;
    [SerializeField] TextMeshProUGUI expansionScrollTxt;

    public void Start()
    {
        myStats = GetComponent<Hec_Stats>();
        myUpdateBarScp = GetComponent<UI_UpdateBars>();
        myChangeTxtScp = GetComponent<UI_ChangeAText>();
    }

    
}
