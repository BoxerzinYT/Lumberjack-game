using UnityEngine;

public class Hec_ActionSystem : MonoBehaviour
{
    Hec_Stats myStats;
    bool isInActionMode;
    ActionBlock[] blocks;

    public void Start()
    {
        myStats = GetComponent<Hec_Stats>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isInActionMode)
        {
            blocks = FindObjectsByType<ActionBlock>(0);
            foreach(var a in blocks)
            {
                a.ActivateActionBlock(myStats);
            }
            
            isInActionMode = true;
        }
        else if (Input.GetKeyUp(KeyCode.Q) && isInActionMode)
        {
            foreach(var a in blocks)
            {
                if(a!= null)
                {
                    a.DesactivateBlock();
                }
            }
            blocks = null;
            isInActionMode = false;
        }
    }
}
