using UnityEngine;

public class Struc_ExpeditionTrainKey : MonoBehaviour
{
    [SerializeField] Struc_ExpeditionPanel myExpeditionPanel;

    public void Update()
    {
        myExpeditionPanel.PreviousPhase();
    }
}
