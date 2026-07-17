using UnityEngine;
using UnityEngine.UI;

public class Power_PassiveManager : MonoBehaviour
{
    [SerializeField] Hec_Stats hecStats;
    public Hec_Stats HecStats { get { return hecStats; }}
    [SerializeField] float powerPoints;
    public float PowerPoints { get { return powerPoints; }
        set
        {
            powerPoints = value;
            if(powerPoints >= maxPowerPoints)
            {
                powerPoints = maxPowerPoints;
            }
            else if(powerPoints < 0)
            {
                powerPoints = 0;
            }
            UpdatePowerUI();
        } }
    [SerializeField] float maxPowerPoints;
    [SerializeField] Image starLoading;
    public void Awake()
    {
        Breakable_Tree.tree_whenTakeDamage += () => PowerPoints += 0.25f;
        Breakable_Tree.tree_whenDie += () => PowerPoints += 2;
    }

    public void UpdatePowerUI()
    {
        EventsManager.eventM.UpdateBar(powerPoints, maxPowerPoints, starLoading, null);
    }
}
