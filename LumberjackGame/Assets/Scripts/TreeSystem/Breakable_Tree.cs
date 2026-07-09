using UnityEngine;
using UnityEngine.UI;

public class Breakable_Tree : BreakableObj
{
    [Header("0=Baby, 1=Medium, 2=High")]
    [SerializeField] int phase;
    [SerializeField] int maxPhase;
    [SerializeField] float[] treeScale;
    [SerializeField] float[] treeLife;
    
    [Header("LifeBar")]
    [SerializeField] GameObject lifeBar;
    [SerializeField] Image lifeBar_bar;
    [SerializeField] Gradient lifeBar_gradient;

    void Start()
    {
        SetChangesForTreeByPhase();
    }

    public override void CallDrop(Hec_Stats hec_stats)
    {
        MyDropSystem.Drop(hec_stats, phase);
    }

    public void SetChangesForTreeByPhase()
    {
        transform.localScale = new Vector3(treeScale[phase], treeScale[phase], treeScale[phase]);
        ChangeMaxLife(treeLife[phase], true);
        UpdateMyLifeBar();
        //Debug.Log(Life);
    }

    public void UpdateMyLifeBar()
    {
        if(Life >= treeLife[phase]) { lifeBar.SetActive(false); }
        else { lifeBar.SetActive(true); }
        EventsManager.eventM.UpdateBar(Life, treeLife[phase], lifeBar_bar, lifeBar_gradient);
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
}
