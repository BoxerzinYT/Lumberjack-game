using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShowInventoryItens))]
public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] ShowInventoryItens sii;
    [SerializeField] InventoryInfoUIManager inventInfoMan;
    HectorInventory hjInvent;
    //bool settingsModeEnabled = false;

    public void OnEnable()
    {
        if(hjInvent == null) { hjInvent = GameObject.FindFirstObjectByType<HectorInventory>(); }
        sii.ShowItensInUI(hjInvent.hectorInventory.inventory);
    }

    public void FinishSettingTheItens()
    {
        foreach (var savedItens in sii.ItensPrefabed)
        {
            savedItens.ItemPrefab.GetComponent<InventoryItemUIManager>().FinishSettings(true, savedItens.inventItem, savedItens.inventItem.itemData, savedItens.inventItem.stackSize);

            savedItens.ItemPrefab.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            savedItens.ItemPrefab.GetComponent<Button>().onClick.AddListener(() => inventInfoMan.DisplayInfo(savedItens.inventItem));
            
        }
    }

    /*
    public void EnableSettingsMode()
    {
        if(settingsModeEnabled == false)
        {
            foreach (var savedItens in sii.ItensPrefabed)
            {
                savedItens.ItemPrefab.GetComponent<InventoryItemUIManager>().SettingsModeActivated();
            }
            settingsModeEnabled = true;
        }
        else if(settingsModeEnabled == true)
        {
            foreach (var savedItens in sii.ItensPrefabed)
            {
                savedItens.ItemPrefab.GetComponent<InventoryItemUIManager>().SettingsModeDesactivated();
            }
            settingsModeEnabled = false;
        }
    }
    */
}