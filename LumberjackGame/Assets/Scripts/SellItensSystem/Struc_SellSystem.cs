using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Struc_SellSystem : StructureSystem
{
    HectorInventory hjInvent;
    Hec_Stats hj;
    ShowInventoryItens sii;
    [SerializeField] Canvas myHUD;
    [SerializeField] SelectQuant selectQuantUI;
    [SerializeField] Transform sellContent;
    [SerializeField] TextMeshProUGUI totalValueOfSellTxt;
    [SerializeField] List<ItensInSell> ItensSell = new List<ItensInSell>();
    [SerializeField] GameObject sellButton;
    [SerializeField] GameObject clearButton;
    float totalValueOfSell;

    private void Start()
    {
        sii = GetComponent<ShowInventoryItens>();

        UpdateTotalValueOfSellTxt();
    }

    public void WhenOpenPanel()
    {
        hj = LastPlayerThatPassHere;
        sii.ShowItensInUI(hj.Hec_invent.hectorInventory.inventory);
    }

    public void SelectQuantForSell(int itemIndex, InventoryItem ii, GameObject inSellGob, InventoryItemUIManager inventUiMan, Sell_Item sell_item)
    {
        //inventUiMan.transform.SetParent(inventUiMan.ParentBeforeDrag);
        SelectQuant newSQUI = Instantiate(selectQuantUI, myHUD.transform);

        newSQUI.SelectButton.onClick.AddListener(() => SelectedQuant(itemIndex, newSQUI.GetValue(), ii, inSellGob, inventUiMan, sell_item));
        newSQUI.Slider.maxValue = ii.stackSize;
    }

    public void SelectedQuant(int itemIndex, int quantOfItens, InventoryItem ii, GameObject inSellGob, InventoryItemUIManager inventUiMan, Sell_Item sell_item)
    {
        InventoryItemUIManager newItemPrefab = Instantiate(sii.ItemPrefab.GetComponent<InventoryItemUIManager>(), sellContent);
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

        ItensInSell itemS = new ItensInSell(ii, quantOfItens, realValue, sell_item.itemSellXp, inSellGob, inventUiMan, newItemPrefab);
        ItensSell.Add(itemS);

        //newItemPrefab.WhenDropInSomePlace = new UnityEngine.Events.UnityEvent();
        newItemPrefab.MyButton.onClick.AddListener(() => DeselectSellItem(itemIndex, quantOfItens, itemS, sell_item, newItemPrefab, inSellGob, inventUiMan));

        inSellGob.gameObject.SetActive(true);
        inventUiMan.transform.Find("sp").transform.Find("QuantityTxt").gameObject.SetActive(false);
        //inventUiMan.CanDrag = false;
        //inventUiMan.OnEndDragManual(inventUiMan.ParentBeforeDrag);

        sellButton.gameObject.SetActive(true);
        clearButton.gameObject.SetActive(true);

        UpdateTotalValueOfSellTxt();
    }

    public void DeselectSellItem(int itemIndex, int quantOfItens, ItensInSell itemS, Sell_Item sell_item, InventoryItemUIManager newItemPrefab, GameObject inSellGob, InventoryItemUIManager inventUiMan)
    {
        Destroy(newItemPrefab.gameObject, 0.01f);
        inSellGob.gameObject.SetActive(false);
        inventUiMan.transform.Find("sp").transform.Find("QuantityTxt").gameObject.SetActive(true);
        //inventUiMan.CanDrag = true;
        inventUiMan.Selling = false;

        float quantOfItensValue = quantOfItens * sell_item.itemValue;
        totalValueOfSell -= quantOfItensValue;

        sellButton.gameObject.SetActive(false);
        clearButton.gameObject.SetActive(false);

        ItensSell.Remove(itemS);

        UpdateTotalValueOfSellTxt();
    }

    public void UpdateTotalValueOfSellTxt() { totalValueOfSellTxt.text = "Total: " + EventsManager.eventM.UpdateVariables(totalValueOfSell); }

    public void Sell()
    {
        if(ItensSell.Count > 0)
        {
            float totalCoins = 0;
            float totalXp = 0;

            foreach (var itensS in ItensSell)
            {
                totalCoins += itensS.quantOfSell * itensS.yourStack;
                totalXp += Random.Range(itensS.xpSell[0], itensS.xpSell[1]) * itensS.yourStack;

                hj.Hec_invent.hectorInventory.RemoveItem(itensS.yourStack, itensS.item);
            }

            hj.hec_coins += totalCoins;
            hj.hec_xp += totalXp;

            WhenOpenPanel();

            Clear();
        }
    }

    public void Clear()
    {
        if(ItensSell.Count > 0)
        {
            foreach (Transform t in sellContent)
            {
                Destroy(t.gameObject);
            }
            foreach (var s in ItensSell)
            {
                s.inSellGob.gameObject.SetActive(false);
                s.inventUIMan.transform.Find("sp").transform.Find("QuantityTxt").gameObject.SetActive(true);
                //s.inventUIMan.CanDrag = true;
            }
            totalValueOfSell = 0;
            ItensSell.Clear();

            //sellButton.gameObject.SetActive(false);
            //clearButton.gameObject.SetActive(false);

            UpdateTotalValueOfSellTxt();
        }
    }

    public void FinishSettingsOfItens()
    {
        int i = 0;
        foreach(var savedItens in sii.ItensPrefabed)
        {
            if(savedItens.inventItem.itemData.GetType() != typeof(Sell_Item))
            {
                Destroy(savedItens.ItemPrefab);
                //sii.ItensPrefabed.Remove(savedItens);
                continue;
            }
            //if(savedItens.inventItem.locked == true) { Destroy(savedItens.ItemPrefab); continue; }

            InventoryItemUIManager inventUiMan = savedItens.ItemPrefab.GetComponent<InventoryItemUIManager>();

            inventUiMan.FinishSettings(true, savedItens.inventItem, null, 0);

            GameObject inSellGob = inventUiMan.transform.Find("sp").transform.Find("SellSelected").gameObject;

            Sell_Item mtrl = (Sell_Item)savedItens.inventItem.itemData;

            //inventUiMan.WhenDropInSomePlace = new UnityEngine.Events.UnityEvent();
            if(savedItens.inventItem.stackSize > 1)
            {
                inventUiMan.MyButton.onClick.AddListener(() => SelectQuantForSell(i, savedItens.inventItem, inSellGob, inventUiMan, mtrl));
            }
            else if(savedItens.inventItem.stackSize <= 1)
            {
                inventUiMan.MyButton.onClick.AddListener(() => SelectedQuant(i, 1, savedItens.inventItem, inSellGob, inventUiMan, mtrl));
            }
        }
    }

}
[System.Serializable]

public sealed class ItensInSell
{
    public InventoryItem item;
    public GameObject inSellGob;
    public InventoryItemUIManager inventUIMan;
    public InventoryItemUIManager newItemPrefab;
    public int yourStack;
    public float quantOfSell;
    public float[] xpSell;

    public ItensInSell(InventoryItem ii, int _ys, float _quantOfSell, float[] _xpSell, GameObject inSellGob, InventoryItemUIManager inventUIMan, InventoryItemUIManager newItemPrefab)
    {
        item = ii;
        yourStack = _ys;
        quantOfSell = _quantOfSell;
        xpSell = _xpSell;
        this.inSellGob = inSellGob;
        this.inventUIMan = inventUIMan;
        this.newItemPrefab = newItemPrefab;
    }
}
