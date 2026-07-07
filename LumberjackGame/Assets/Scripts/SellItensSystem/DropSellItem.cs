using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSellItem : MonoBehaviour, IDropHandler
{
    [SerializeField] bool selling;
    //[SerializeField] SellMachineManager sellmman;
    HectorInventory hjInvent;
    Hec_Mov hj;

    void Start()
    {

        hjInvent = hj.GetComponent<HectorInventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventoryItemUIManager itemDropped = eventData.pointerDrag.GetComponent<InventoryItemUIManager>();
            if(itemDropped.Selling == selling)
            {
                //itemDropped.WhenDropInSomePlace.Invoke();
            }
        }
    }
}
