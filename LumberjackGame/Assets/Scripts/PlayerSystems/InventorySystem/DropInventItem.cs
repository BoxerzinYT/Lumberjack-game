using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropInventItem : MonoBehaviour, IDropHandler
{
    [SerializeField] ShowInventoryItens sii;
    [SerializeField] SelectQuant selectQuantUI;
    HectorInventory hjInvent;
    Hec_Mov hj;

    void Start()
    {
        hj = FindFirstObjectByType<Hec_Mov>();
        hjInvent = hj.GetComponent<HectorInventory>();
    }

    

    public void OnDrop(PointerEventData eventData)
    {
        /*
        if (eventData.pointerDrag != null)
        {
            //Debug.Log("Dropped");
            InventoryItemUIManager itemDropped = eventData.pointerDrag.GetComponent<InventoryItemUIManager>();

            if(itemDropped.MyInventItem.locked == false)
            {
                if (itemDropped.MyInventItem.stackSize > 1)
                {
                    SelectQuant newSQUI = Instantiate(selectQuantUI, FindFirstObjectByType<UI_PlayerUI>().transform);

                    newSQUI.SelectButton.onClick.AddListener(() => DropItemInGround(itemDropped, newSQUI.GetValue()));
                    newSQUI.Slider.maxValue = itemDropped.MyInventItem.stackSize;
                }
                else
                {
                    DropItemInGround(itemDropped, 1);
                }
            }
        }
        */
    }

    
    public void DropItemInGround(InventoryItemUIManager itemDropped, int quantForDrop)
    {
        /*
        ItemDropped dropped = Instantiate(itemDropped.MyItem.dropItem);
        //dropped.GetComponent<DropMov>().SetDrop(hj.transform, true);
        dropped.QuantityHere = quantForDrop;
        dropped.transform.position = new Vector3(hj.transform.position.x, hj.transform.position.y);

        hjInvent.hectorInventory.RemoveItem(quantForDrop, itemDropped.MyInventItem);

        if(quantForDrop <= 1)
        {
            Destroy(itemDropped.gameObject, 0.0001f);
        }

*/
    }
    
}