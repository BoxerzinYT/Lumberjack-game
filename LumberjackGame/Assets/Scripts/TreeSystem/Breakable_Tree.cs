using System;
using UnityEngine;
using UnityEngine.UI;

public class Breakable_Tree : BreakableObj
{
    InventoryItem mySappling;
    public void AddSappling(InventoryItem inventItem)
    {
        mySappling = inventItem;
    }
    public static event Action tree_whenDie;
    public static event Action tree_whenTakeDamage;
    [Header("0=Baby, 1=Medium, 2=High")]
    [SerializeField] int phase;
    [SerializeField] int maxPhase;
    [SerializeField] float[] treeScale;
    [SerializeField] float[] treeLife;

    public override void CallDrop(Hec_Stats hec_stats)
    {
        MyDropSystem.Drop(hec_stats, phase, mySappling.itemRank.rankMultiplier);
    }

    public void SetChangesForTreeByPhase()
    {
        transform.localScale = new Vector3(treeScale[phase], treeScale[phase], treeScale[phase]);
        ChangeMaxLife(treeLife[phase] * mySappling.itemRank.rankMultiplier, true);
        UpdateMyLifeBar();
        //Debug.Log(Life);
    }

    public override void UpdateMyLifeBar()
    {
        if(Life >= treeLife[phase] * mySappling.itemRank.rankMultiplier) { LifeBar.SetActive(false); }
        else if(Life <= 0) { LifeBar.SetActive(false); }
        else { LifeBar.SetActive(true); }
        EventsManager.eventM.UpdateBar(Life, treeLife[phase] * mySappling.itemRank.rankMultiplier, LifeBar_bar, LifeBar_gradient);
        //Debug.Log(Life >= treeLife[phase]);
        //Debug.Log(Life + "/" + treeLife[phase]);
    }
    public void ChangePhase(int newV)
    {
        phase += newV;
        if(phase >= maxPhase) { phase = maxPhase; } else if(phase < 0) { phase = 0; }
        SetChangesForTreeByPhase();
    }
    public bool HasPhaseFull() { return phase >= maxPhase; }

    public void ActivateActionWhenDie()
    {
        tree_whenDie?.Invoke();
    }
    public void ActivateWhenTakeDamage()
    {
        tree_whenTakeDamage?.Invoke();
    }
}
