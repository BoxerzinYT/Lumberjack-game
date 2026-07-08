using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Struc_Compost : StructureSystem
{
    [Header("ComposterSettings")]
    [SerializeField] SelectQuant selectQuant;
    [SerializeField] Canvas myHUD;
    [SerializeField] ShowInventoryItens showInventItens;
    [SerializeField] List<InventoryItem> toCompostList = new List<InventoryItem>();
    [SerializeField] float treesRange;
    [SerializeField] LayerMask breakableObjLayer;

    [SerializeField] int capacity;
    int hipoteticalCapacity;
    [SerializeField] int maxCapacity;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI capacityTxt;
    [SerializeField] TextMeshProUGUI hipotetical_CapacityTxt;
    [SerializeField] Image composterBar;
    [SerializeField] Image hipotetical_ComposterBar;
    [SerializeField] Gradient composterBarGradient;
    public void UpdateInventory()
    {
        showInventItens.ShowItensInUI(LastPlayerThatPassHere.Hec_invent.hectorInventory.inventory);
    }

    public void WhenOpenPanel()
    {
        UpdateCapacityBar();
        UpdateHipoteticalCapacityBar();
        UpdateInventory();
    }

    public void DepositTrash()
    {
        List<Breakable_Tree> TreesFounded = new List<Breakable_Tree>();
        TreesFounded = ReturnTrees();

        int fullTrees = 0; //Var para definir quantas arvores no range da composteira estão em sua fase maxima
        foreach(var t in TreesFounded)
        {
            if(t.HasPhaseFull()){ fullTrees++; }
        }
        if(fullTrees == TreesFounded.Count){ Debug.Log("All trees maxed!"); return; }
        foreach(var i in toCompostList)
        {
            LastPlayerThatPassHere.Hec_invent.hectorInventory.RemoveItem(i.stackSize, i);
        }

        capacity += hipoteticalCapacity;
        if(capacity >= maxCapacity)
        {
            hipoteticalCapacity = capacity - maxCapacity;
        }
        else
        {
            hipoteticalCapacity -= capacity;
        }
        if(capacity >= maxCapacity)
        {
            foreach(var t in TreesFounded)
            {
                t.ChangePhase(1);
            }
            capacity = 0;
        }

        toCompostList.Clear();
        WhenOpenPanel();
    }
    public List<Breakable_Tree> ReturnTrees()
    {
        List<Breakable_Tree> TreesFounded = new List<Breakable_Tree>();
        Collider2D[] foundTreesArea = Physics2D.OverlapCircleAll(transform.position, treesRange, breakableObjLayer);
        foreach(var f in foundTreesArea)
        {
            if (f.GetComponent<Breakable_Tree>())
            {
                TreesFounded.Add(f.GetComponent<Breakable_Tree>());
            }
        }

        return TreesFounded;
    }

    public void FinishSettings()
    {
        foreach(var s in showInventItens.ItensPrefabed)
        {
            GameObject inSellGob = s.ItemPrefab.transform.Find("sp").transform.Find("SellSelected").gameObject;
            s.ItemPrefab.MyButton.onClick = new Button.ButtonClickedEvent();
            if(s.inventItem.stackSize > 1)
            {
                s.ItemPrefab.MyButton.onClick.AddListener(() => SelectQuantForDeposit(s.inventItem, inSellGob, s.ItemPrefab));
            }
            else if(s.inventItem.stackSize <= 1)
            {
                s.ItemPrefab.MyButton.onClick.AddListener(() => SelectedQuantForDeposit(1, s.inventItem, inSellGob, s.ItemPrefab));
            }
        }
    }

    public void SelectedQuantForDeposit(int quant, InventoryItem ii, GameObject inSellGob, InventoryItemUIManager inventUiMan)
    {
        if(hipoteticalCapacity >= maxCapacity) { return; }
        InventoryItem itemToList = new InventoryItem(LastPlayerThatPassHere.Hec_invent.hectorInventory.GetIDOfAItem(ii.itemData), ii.itemData, quant);
        hipoteticalCapacity += quant;
        toCompostList.Add(itemToList);
        inSellGob.gameObject.SetActive(true);

        UpdateHipoteticalCapacityBar();

        inventUiMan.MyButton.onClick = new Button.ButtonClickedEvent();
        inventUiMan.MyButton.onClick.AddListener(() => DeselectQuant(itemToList, inSellGob, inventUiMan));
    }

    public void SelectQuantForDeposit(InventoryItem ii, GameObject inSellGob, InventoryItemUIManager inventUiMan)
    {
        if(hipoteticalCapacity >= maxCapacity) { return; }
        //inventUiMan.transform.SetParent(inventUiMan.ParentBeforeDrag);
        SelectQuant newSQUI = Instantiate(selectQuant, myHUD.transform);

        newSQUI.SelectButton.onClick.AddListener(() => SelectedQuantForDeposit(newSQUI.GetValue(), ii, inSellGob, inventUiMan));
        if(ii.stackSize + hipoteticalCapacity >= maxCapacity)
        {
            newSQUI.Slider.maxValue = maxCapacity - hipoteticalCapacity;
        }
        else
        {
            newSQUI.Slider.maxValue = ii.stackSize;
        }
    }

    public void DeselectQuant(InventoryItem ii, GameObject inSellGob, InventoryItemUIManager inventUiMan)
    {
        hipoteticalCapacity -= ii.stackSize;
        toCompostList.Remove(ii);
        inSellGob.SetActive(false);

        inventUiMan.MyButton.onClick = new Button.ButtonClickedEvent();
        if(ii.stackSize > 1)
        {
            inventUiMan.MyButton.onClick.AddListener(() => SelectQuantForDeposit(ii, inSellGob, inventUiMan));
        }
        else if(ii.stackSize <= 1)
        {
            inventUiMan.MyButton.onClick.AddListener(() => SelectedQuantForDeposit(1, ii, inSellGob, inventUiMan));
        }

        UpdateHipoteticalCapacityBar();
    }

    public void UpdateHipoteticalCapacityBar()
    {
        EventsManager.eventM.ChangeAText(hipotetical_CapacityTxt, hipoteticalCapacity + "/" + maxCapacity);
        EventsManager.eventM.UpdateBar(hipoteticalCapacity, maxCapacity, hipotetical_ComposterBar, composterBarGradient);
    }
    public void UpdateCapacityBar()
    {
        EventsManager.eventM.ChangeAText(capacityTxt, capacity + "/" + maxCapacity);
        EventsManager.eventM.UpdateBar(capacity, maxCapacity, composterBar, composterBarGradient);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, treesRange);
    }
}
