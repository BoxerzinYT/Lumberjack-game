using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShowInventoryItens : MonoBehaviour
{
    [SerializeField] Inventory hec_invent;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform content;
    [SerializeField] UnityEvent changeSomethingInItens;
    [SerializeField] List<ItemList> itensPrefabed = new List<ItemList>();
    public List<ItemList> ItensPrefabed { get { return itensPrefabed; } }
    public UnityEvent ChangeSomethingInItens { get { return changeSomethingInItens; } set { changeSomethingInItens = value; } }
    public Transform Content { get { return content; } set { content = value; } }
    public GameObject ItemPrefab { get { return itemPrefab; } set { itemPrefab = value; } }

    public void ResetInventory()
    {
        ItensPrefabed.Clear();

        if (content != null)
        {
            foreach (Transform childT in content)
            {
                Destroy(childT.gameObject);
            }
        }
    }

    public void ShowItensInUI(List<InventoryItem> inventory)
    {
        ResetInventory();

        if (content != null)
        {
            foreach (InventoryItem InventItem in inventory)
            {
                GameObject newSlot = Instantiate(itemPrefab, content);
                itensPrefabed.Add(new ItemList(newSlot, InventItem));
                newSlot.GetComponent<InventoryItemUIManager>().FinishSettings(true, InventItem, null, 0);
                newSlot.GetComponent<InventoryItemUIManager>().Sii = this;
             }
            
        }

        if(changeSomethingInItens != null)
        {
            changeSomethingInItens.Invoke();
        }
    }

    public void CallShowItensInUI()
    {
        ShowItensInUI(hec_invent.inventory);
    }
}
[System.Serializable]
public sealed class ItemList
{
    public GameObject ItemPrefab;
    public InventoryItem inventItem;

    public ItemList(GameObject itemS, InventoryItem _item)
    {
        ItemPrefab = itemS;
        inventItem = _item;
    }
}
