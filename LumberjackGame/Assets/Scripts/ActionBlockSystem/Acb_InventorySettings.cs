using UnityEngine;

public class Acb_InventorySettings : MonoBehaviour
{
    [SerializeField] Hec_ActionSystem hec_acbSystem;
    [SerializeField] ShowInventoryItens showInventItens;

    public void FinishItensSettings()
    {
        foreach(var s in showInventItens.ItensPrefabed)
        {
            if (s.inventItem.itemData.GetType() == typeof(ActionItem))
            {
                ActionItem acbItem = (ActionItem)s.inventItem.itemData;
                s.ItemPrefab.MyButton.onClick.AddListener(() => hec_acbSystem.StartAction(acbItem, s.inventItem));
            }
            else
            {
                continue;
            }
        }
    }

}
