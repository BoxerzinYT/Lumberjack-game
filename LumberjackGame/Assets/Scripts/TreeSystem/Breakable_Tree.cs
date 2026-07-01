using UnityEngine;

public class Breakable_Tree : BreakableObj
{
    [Header("0=Baby, 1=Medium, 2=High")]
    [SerializeField] int phase;
    [SerializeField] int maxPhase;
    [SerializeField] float[] treeScale;
    [SerializeField] float[] treeLife;

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
        //Debug.Log(Life);
    }

    public void ChangePhase(int newV)
    {
        phase += newV;
        if(phase >= maxPhase) { phase = maxPhase; } else if(phase < 0) { phase = 0; }
        SetChangesForTreeByPhase();
    }
    public bool HasPhaseFull() { return phase >= maxPhase; }
}
