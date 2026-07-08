using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Struc_SellSystem : StructureSystem
{
    HectorInventory hjInvent;
    Hec_Stats hj;
    [SerializeField] ShowInventoryItens sii;
    [SerializeField] Canvas myHUD;
    [SerializeField] SelectQuant selectQuantUI;
    [SerializeField] Transform sellContent;
    [SerializeField] TextMeshProUGUI totalValueOfSellTxt;
    [SerializeField] List<ItensInSell> ItensSellList = new List<ItensInSell>();
    [SerializeField] GameObject sellButton;
    [SerializeField] GameObject clearButton;
    float totalValueOfSell;

    public void WhenOpenPanel()
    {
        hj = LastPlayerThatPassHere;
        sii.ShowItensInUI(hj.Hec_invent.hectorInventory.inventory);

        foreach(var s in ItensSellList)
        {
            Destroy(s.itemInTable_ItemPrefab);
        }

        ItensSellList.Clear();
        totalValueOfSell = 0;

        UpdateTotalValueOfSellTxt();
    }

    public void UpdateVariables()
    {
        UpdateTotalValueOfSellTxt();
    }

    public void SelectQuantForSell(InventoryItem ii, GameObject inSellGob, InventoryItemUIManager inventUiMan, Sell_Item sell_item)
    {
        //inventUiMan.transform.SetParent(inventUiMan.ParentBeforeDrag);
        SelectQuant newSQUI = Instantiate(selectQuantUI, myHUD.transform);

        newSQUI.SelectButton.onClick.AddListener(() => SelectedQuant(newSQUI.GetValue(), ii, inSellGob, inventUiMan, sell_item));
        newSQUI.Slider.maxValue = ii.stackSize;
    }

    public void SelectedQuant(int quantOfItens, InventoryItem ii, GameObject inSellGob, InventoryItemUIManager inventUiMan, Sell_Item sell_item)
    {
        InventoryItemUIManager newItemPrefab = Instantiate(sii.ItemPrefab, sellContent);
        newItemPrefab.FinishSettings(true, ii, null, 0);
        newItemPrefab.transform.Find("sp").transform.Find("QuantityTxt").GetComponent<TextMeshProUGUI>().text = "x" + EventsManager.eventM.UpdateVariables((float)quantOfItens);
        newItemPrefab.Selling = true;

        float priceBoost = 1f;
        /*

        if (ii.enchants.Count > 0)
        {
            foreach (var i in ii.enchants)
            {
                if (i.enchantId == 5)
                {
                    priceBoost += i.add;
                }
                else { continue; }
            }
        }
        */

        float realValue = sell_item.itemValue * priceBoost;

        float quantOfItensValue = quantOfItens * realValue;
        totalValueOfSell += quantOfItensValue;

        ItensInSell itemS = new ItensInSell(ii, quantOfItens, realValue, sell_item.itemSellXp, inSellGob, inventUiMan, newItemPrefab.gameObject);
        ItensSellList.Add(itemS);

        //newItemPrefab.WhenDropInSomePlace = new UnityEngine.Events.UnityEvent();
        newItemPrefab.MyButton.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
        newItemPrefab.MyButton.onClick.AddListener(() => DeselectSellItem(quantOfItens, itemS, sell_item, newItemPrefab.gameObject, inSellGob, inventUiMan));

        inSellGob.gameObject.SetActive(true);
        inventUiMan.transform.Find("sp").transform.Find("QuantityTxt").gameObject.SetActive(false);
        //inventUiMan.CanDrag = false;
        //inventUiMan.OnEndDragManual(inventUiMan.ParentBeforeDrag);

        sellButton.gameObject.SetActive(true);
        clearButton.gameObject.SetActive(true);

        UpdateTotalValueOfSellTxt();
    }

    public void DeselectSellItem(int quantOfItens, ItensInSell itemS, Sell_Item sell_item, GameObject newItemPrefab, GameObject inSellGob, InventoryItemUIManager inventUiMan)
    {
        Destroy(newItemPrefab, 0.01f);
        inSellGob.gameObject.SetActive(false);
        inventUiMan.transform.Find("sp").transform.Find("QuantityTxt").gameObject.SetActive(true);
        //inventUiMan.CanDrag = true;
        inventUiMan.Selling = false;

        float quantOfItensValue = quantOfItens * sell_item.itemValue;
        totalValueOfSell -= quantOfItensValue;

        sellButton.gameObject.SetActive(false);
        clearButton.gameObject.SetActive(false);

        foreach(var s in ItensSellList)
        {
            if(s.sellItem_inventItem == itemS.sellItem_inventItem)
            {
                ItensSellList.Remove(s);
                break;
            }
        }

        UpdateVariables();
    }

    public void UpdateTotalValueOfSellTxt() { totalValueOfSellTxt.text = "Total: " + EventsManager.eventM.UpdateVariables(totalValueOfSell); }

    public void Sell()
    {
        if(ItensSellList.Count > 0)
        {
            float totalCoins = 0;
            float totalXp = 0;

            foreach (var itensS in ItensSellList)
            {
                totalCoins += itensS.sellItem_sellValue * itensS.sellItem_QuantOfItems;
                totalXp += Random.Range(itensS.sellItem_xpValue[0], itensS.sellItem_xpValue[1]) * itensS.sellItem_QuantOfItems;

                hj.Hec_invent.hectorInventory.RemoveItem(itensS.sellItem_QuantOfItems, itensS.sellItem_inventItem);
            }

            hj.hec_coins += totalCoins;
            hj.hec_xp += totalXp;

            WhenOpenPanel();

            Clear();
        }
    }

    public void Clear()
    {
        if(ItensSellList.Count > 0)
        {
            foreach (Transform t in sellContent)
            {
                Destroy(t.gameObject);
            }
            foreach (var s in ItensSellList)
            {
                s.sellItem_inSellGobj.gameObject.SetActive(false);
                s.sellItem_inventUIMan.transform.Find("sp").transform.Find("QuantityTxt").gameObject.SetActive(true);
                //s.inventUIMan.CanDrag = true;
            }
            totalValueOfSell = 0;
            ItensSellList.Clear();

            //sellButton.gameObject.SetActive(false);
            //clearButton.gameObject.SetActive(false);

            UpdateTotalValueOfSellTxt();
        }
    }

    public void FinishSettingsOfItens()
    {
        int i = 0;
        //Debug.Log("Finishing Settings");
        foreach(var si in sii.ItensPrefabed)
        {
            //Debug.Log("Settings finished!");
            if(si.inventItem.itemData.GetType() != typeof(Sell_Item))
            {
                Destroy(si.ItemPrefab.gameObject);
                //sii.ItensPrefabed.Remove(savedItens);
                continue;
            }
            //if(savedItens.inventItem.locked == true) { Destroy(savedItens.ItemPrefab); continue; }

            InventoryItemUIManager inventUiMan = si.ItemPrefab;

            //inventUiMan.FinishSettings(true, savedItens.inventItem, null, 0);

            GameObject inSellGob = inventUiMan.gameObject.transform.Find("sp").transform.Find("SellSelected").gameObject;

            Sell_Item mtrl = (Sell_Item)si.inventItem.itemData;
            inventUiMan.MyButton.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();

            //inventUiMan.WhenDropInSomePlace = new UnityEngine.Events.UnityEvent();
            if(si.inventItem.stackSize > 1)
            {
                si.ItemPrefab.MyButton.onClick.AddListener(() => SelectQuantForSell(si.inventItem, inSellGob, inventUiMan, mtrl));
            }
            else if(si.inventItem.stackSize <= 1)
            {
                si.ItemPrefab.MyButton.onClick.AddListener(() => SelectedQuant(1, si.inventItem, inSellGob, inventUiMan, mtrl));
            }
            i++;
        }
    }

}
[System.Serializable]

public sealed class ItensInSell
{
    public InventoryItem sellItem_inventItem;
    public GameObject sellItem_inSellGobj;
    public InventoryItemUIManager sellItem_inventUIMan;
    public int sellItem_QuantOfItems;
    public float sellItem_sellValue;
    public float[] sellItem_xpValue;

    public GameObject itemInTable_ItemPrefab;

    public ItensInSell(InventoryItem ii, int _ys, float _quantOfSell, float[] _xpSell, GameObject inSellGob, InventoryItemUIManager inventUIMan, GameObject newItemPrefab)
    {
        sellItem_inventItem = ii;
        sellItem_QuantOfItems = _ys;
        sellItem_sellValue = _quantOfSell;
        sellItem_xpValue = _xpSell;
        sellItem_inSellGobj = inSellGob;
        this.sellItem_inventUIMan = inventUIMan;
        itemInTable_ItemPrefab = newItemPrefab;
    }
}
