using UnityEngine;

public class ShowItensWithInventItemManager : MonoBehaviour
{
    HectorInventory hjInvent;

    public InventoryItemUIManager ShowItemAndReturnIt(GameObject obj, Item myItem, int quant)
    {
        if(hjInvent == null) { hjInvent = GameObject.FindFirstObjectByType<HectorInventory>(); }
        obj.SetActive(true);
        InventoryItemUIManager inventUI = obj.GetComponent<InventoryItemUIManager>();

        InventoryItem newInventItem = new InventoryItem(hjInvent.hectorInventory.GetIDOfAItem(myItem),
        myItem, quant);

        //inventUI.CanDrag = false;
        inventUI.IsAShowItem = true;
        inventUI.FinishSettings(true, newInventItem, newInventItem.itemData, newInventItem.stackSize);
        inventUI.StackTxt.gameObject.SetActive(true);

        return inventUI;
    }
}
