using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

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

    public void Start()
    {
        myStats = GetComponent<Hec_Stats>();
        myUpdateBarScp = GetComponent<UI_UpdateBars>();
        myChangeTxtScp = GetComponent<UI_ChangeAText>();
        CallUpdateBar();
    }

    public void CallUpdateBar()
    {
        myUpdateBarScp.UpdateBar(myStats.Hec_Capacity, myStats.Hec_MaxCapacity, hec_capacityBar, capacityBarGradient, true, hec_capacityTxt);
    }

    public void CallSaplingsChange()
    {
        myChangeTxtScp.ChangeAText(saplingsTxt, myStats.MapleTree_saplings_prot.ToString() + "x");
    }

    
}
