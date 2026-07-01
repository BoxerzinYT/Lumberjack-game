using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Struc_Compost : StructureSystem
{
    UI_UpdateBars myUpdateBar;
    [SerializeField] int compost_capacity;
    [SerializeField] int compost_maxCapacity;

    [Header("UI")]
    [SerializeField] Image compostBar;
    [SerializeField] Gradient compostBarGradient;

    public void Start()
    {
        myUpdateBar = GetComponent<UI_UpdateBars>();
        myUpdateBar.UpdateBar(compost_capacity, compost_maxCapacity, compostBar, compostBarGradient, false, null);
    }

    public void DepositTrash()
    {
        if(compost_capacity < compost_maxCapacity)
        {
            compost_capacity += LastPlayerThatPassHere.Hec_Capacity;
            LastPlayerThatPassHere.Hec_Capacity -= compost_capacity;
            if(compost_capacity >= compost_maxCapacity)
            {
                while(compost_capacity >= compost_maxCapacity)
                {
                    Breakable_Tree[] allTrees = FindObjectsByType<Breakable_Tree>(0);
                    bool canAtLeastOneTreeGrow = false;
                    foreach(var t in allTrees)
                    {
                        if (!t.HasPhaseFull())
                        {
                            canAtLeastOneTreeGrow = true;
                            t.ChangePhase(1);
                        }
                    }
                    compost_capacity -= compost_maxCapacity;
                    if(!canAtLeastOneTreeGrow)
                    {
                        Debug.Log("All trees are maxed!");
                        break;
                    }

                }
            }

            myUpdateBar.UpdateBar(compost_capacity, compost_maxCapacity, compostBar, compostBarGradient, false, null);
        }
    }
}
