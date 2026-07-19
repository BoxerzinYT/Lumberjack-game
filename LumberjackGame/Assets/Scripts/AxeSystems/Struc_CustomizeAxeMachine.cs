using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Struc_CustomizeAxeMachine : StructureSystem
{
    [SerializeField] ShowInventoryItens showInventItems;

    [SerializeField] Image bodyImage;
    [SerializeField] Image headImage;
    [SerializeField] RectTransform headRect;

    [Tooltip("Para todos: 0=Body, 1=Head")]
    InventoryItem[] inventItemSelected = new InventoryItem[2];
    InventoryItemUIManager[] itemPrefabSelected = new InventoryItemUIManager[2];
    AxePart[] axePartSelected = new AxePart[2];
    Image[] inSellObjSelected = new Image[2];
    MultiplicatorSettings[] body_multsSelected;
    MultiplicatorSettings[] head_multsSelected;

    AxePart[] hecStats_selected = new AxePart[2];

    [Header("UI")]
    [SerializeField] MultInfoPanel[] multInfoPanels;
    [SerializeField] GameObject multPanel;
    [SerializeField] Button multInfoButton;
    [SerializeField] RectTransform multInfoButtonArrow;
    [SerializeField] ScrollRect inventoryGridScrollRect;
    bool isOpened;
    [SerializeField] Animator notificationObj;
    [SerializeField] Button applyButton;
    bool notificated;

    public void WhenOpenPanel()
    {
        foreach(var infos in multInfoPanels) { infos.multInfoPanel.SetActive(false); }

        multPanel.SetActive(false);
        multInfoButtonArrow.localEulerAngles = new Vector3(0,0, 90);
        isOpened = false;
        applyButton.gameObject.SetActive(false);

        notificationObj.gameObject.SetActive(false);
        notificated = false;

        hecStats_selected[0] = (AxePart)LastPlayerThatPassHere.Hec_axeManager.Body.itemData;
        hecStats_selected[1] = (AxePart)LastPlayerThatPassHere.Hec_axeManager.Head.itemData;

        inventItemSelected[0] = null;
        inventItemSelected[1] = null;
        axePartSelected[0] = null;
        axePartSelected[1] = null;
        inSellObjSelected[0] = null;
        inSellObjSelected[1] = null;
        body_multsSelected = null;
        head_multsSelected = null;

        showInventItems.ShowItensInUI(LastPlayerThatPassHere.Hec_invent.hectorInventory.inventory);
        SetBody(LastPlayerThatPassHere.Hec_axeManager.Body);
        SetHead(LastPlayerThatPassHere.Hec_axeManager.Head);
    }

    public void AddPartToAxe(InventoryItem inventItem, AxePart axePart, Image sellObj, InventoryItemUIManager itemPrefab)
    {
        notificated = false;
        switch (axePart.partType)
        {
            case AxePartType.body:
            SetBody(inventItem);
            if(inSellObjSelected[0] != null){ inSellObjSelected[0].gameObject.SetActive(false); }
            sellObj.gameObject.SetActive(true);
            inSellObjSelected[0] = sellObj;
            axePartSelected[0] = axePart;
            inventItemSelected[0] = inventItem;
            body_multsSelected = axePart.mults;
            break;
            case AxePartType.head:
            SetHead(inventItem);
            if(inSellObjSelected[1] != null){ inSellObjSelected[1].gameObject.SetActive(false); }
            sellObj.gameObject.SetActive(true);
            inSellObjSelected[1] = sellObj;
            axePartSelected[1] = axePart;
            inventItemSelected[1] = inventItem;
            head_multsSelected = axePart.mults;
            break;
        }
        itemPrefab.MyButton.onClick = new Button.ButtonClickedEvent();
        itemPrefab.MyButton.onClick.AddListener(() => DeselectPartToAxe(inventItem, axePart, sellObj, itemPrefab));
        CalculateChanges();
    }
    public void DeselectPartToAxe(InventoryItem inventItem, AxePart axePart, Image sellObj, InventoryItemUIManager itemPrefab)
    {
        switch (axePart.partType)
        {
            case AxePartType.body:
            SetBody(LastPlayerThatPassHere.Hec_axeManager.Body);
            if(itemPrefabSelected[0] != null)
            {
                inSellObjSelected[0].gameObject.SetActive(false);
                itemPrefabSelected[0].MyButton.onClick = new Button.ButtonClickedEvent();
                itemPrefabSelected[0].MyButton.onClick.AddListener(() => AddPartToAxe(inventItem, axePart, sellObj, itemPrefab));
            }
            sellObj.gameObject.SetActive(false);
            inSellObjSelected[0] = null;
            axePartSelected[0] = null;
            inventItemSelected[0] = null;
            body_multsSelected = null;
            break;
            case AxePartType.head:
            SetHead(LastPlayerThatPassHere.Hec_axeManager.Head);
            if(itemPrefabSelected[1] != null)
            {
                inSellObjSelected[1].gameObject.SetActive(false);
                itemPrefabSelected[1].MyButton.onClick = new Button.ButtonClickedEvent();
                itemPrefabSelected[1].MyButton.onClick.AddListener(() => AddPartToAxe(inventItem, axePart, sellObj, itemPrefab));
            }
            sellObj.gameObject.SetActive(false);
            inSellObjSelected[1] = null;
            axePartSelected[1] = null;
            inventItemSelected[1] = null;
            head_multsSelected = null;
            break;
        }
        itemPrefab.MyButton.onClick = new Button.ButtonClickedEvent();
        itemPrefab.MyButton.onClick.AddListener(() => AddPartToAxe(inventItem, axePart, sellObj, itemPrefab));
        CalculateChanges();
    }

    public void CalculateChanges()
    {
        foreach(var infos in multInfoPanels) { infos.multInfoPanel.SetActive(false); }

        float[] newValues = { 1,1,1,0,0,1,1,0 };
        if(body_multsSelected != null)
        {
            for(int i=0; i<multInfoPanels.Length; i++)
            {
                foreach(var m in body_multsSelected)
                {
                    if(m.mult.multId == i)
                    {
                        float add = m.add * inventItemSelected[0].itemRank.rankMultiplier;
                        newValues[i] += add;
                        break;
                    }
                }
            }
        }
        if(head_multsSelected != null)
        {
            for(int i=0; i<multInfoPanels.Length; i++)
            {
                foreach(var m in head_multsSelected)
                {
                    if(m.mult.multId == i)
                    {
                        float add = m.add * inventItemSelected[1].itemRank.rankMultiplier;
                        newValues[i] += add;
                        break;
                    }
                }
            }
        }
        for(int i=0; i<newValues.Length; i++)
        {
            float equippedValue = LastPlayerThatPassHere.GetAMultValue(i);
            if(newValues[i] == equippedValue) { continue; }
            else
            {  
                if(!notificated)
                {
                    notificationObj.gameObject.SetActive(true);
                    notificationObj.SetTrigger("Notify");
                    notificated = true;
                }
            }
            string oldTxt = "";
            string newTxt = "";
            if(i == 3 || i == 4)
            {
                oldTxt = (equippedValue * 100).ToString("F0") + "%";
                newTxt = (newValues[i] * 100).ToString("F0") + "%";
            }
            else if(i == 7)
            {
                oldTxt = equippedValue.ToString("F2");
                newTxt = newValues[i].ToString("F2");
            }
            else
            {
                oldTxt = equippedValue.ToString("F2") + "x";
                newTxt = newValues[i].ToString("F2") + "x";
            }
            multInfoPanels[i].multInfoPanel.SetActive(true);
            multInfoPanels[i].oldValueTxt.text = oldTxt;
            multInfoPanels[i].newValueTxt.text = newTxt;
            if(equippedValue < newValues[i]) { multInfoPanels[i].newValueTxt.color = Color.green; }
            else if(equippedValue > newValues[i]) { multInfoPanels[i].newValueTxt.color = Color.red; }
            else if(equippedValue == newValues[i]) { multInfoPanels[i].newValueTxt.color = Color.white; }
        }

        if(body_multsSelected != null || head_multsSelected != null) { applyButton.gameObject.SetActive(true); }
        else if(body_multsSelected == null && head_multsSelected == null) { applyButton.gameObject.SetActive(false); }
    }

    public float GetMultValueOfAxePart(int multId, AxePartType axePartType)
    {
        switch (axePartType)
        {
            case AxePartType.body:
            if(hecStats_selected[0].mults.Length != 0)
            {
                foreach(var m in hecStats_selected[0].mults)
                {
                    if(m.mult.multId == multId)
                    {
                        return m.add;
                    }
                }
            }
            else
            {
                return 0;
            }
            break;
            case AxePartType.head:
            if(hecStats_selected[1].mults.Length != 0)
            {
                foreach(var m in hecStats_selected[1].mults)
                {
                    if(m.mult.multId == multId)
                    {
                        return m.add;
                    }
                }
            }
            else
            {
                return 0;
            }
            break;
        }
        return 0;
    }

    public void OpenOrCloseMultInfoPanel()
    {
        if (!isOpened)
        {
            multPanel.SetActive(true);
            //inventoryGridScrollRect.enabled = false;
            multInfoButtonArrow.localEulerAngles = new Vector3(0,0, -90);
            isOpened = true;
        }
        else if (isOpened)
        {
            multPanel.SetActive(false);
            //inventoryGridScrollRect.enabled = true;
            multInfoButtonArrow.localEulerAngles = new Vector3(0,0, 90);
            isOpened = false;
        }
        notificationObj.gameObject.SetActive(false);
    }

    public void ApplyChanges()
    {
        if(body_multsSelected != null)
        {
            CollectItem(LastPlayerThatPassHere, LastPlayerThatPassHere.Hec_axeManager.Body, 1);
            RemoveItem(LastPlayerThatPassHere, inventItemSelected[0], 1);

            LastPlayerThatPassHere.Hec_axeManager.SetNewBody(inventItemSelected[0]);
        }
        if(head_multsSelected != null)
        {
            CollectItem(LastPlayerThatPassHere, LastPlayerThatPassHere.Hec_axeManager.Head, 1);
            RemoveItem(LastPlayerThatPassHere, inventItemSelected[1], 1);

            LastPlayerThatPassHere.Hec_axeManager.SetNewHead(inventItemSelected[1]);
        }
        WhenOpenPanel();
    }

    public void FinishSettings()
    {
        foreach(var i in showInventItems.ItensPrefabed)
        {
            if(i.inventItem.itemData.GetType() == typeof(AxePart))
            {
                AxePart theAxePart = (AxePart)i.inventItem.itemData;
                Image sellObj = i.ItemPrefab.gameObject.transform.Find("sp").transform.Find("SellSelected").GetComponent<Image>();
                sellObj.raycastTarget = false;
                i.ItemPrefab.MyButton.onClick = new Button.ButtonClickedEvent();
                i.ItemPrefab.MyButton.onClick.AddListener(() => AddPartToAxe(i.inventItem, theAxePart, sellObj, i.ItemPrefab));
            }
            else
            {
                Destroy(i.ItemPrefab.gameObject);
            }
        }
    }

    public void SetBody(InventoryItem inventItem)
    {
        AxePart newBody = (AxePart)inventItem.itemData;
        bodyImage.sprite = newBody.partSprite;
        bodyImage.SetNativeSize();
        Vector2 axeHeadPos = headRect.anchoredPosition;
        axeHeadPos.x = newBody.partSprite.rect.width / newBody.partSprite.pixelsPerUnit * 0.25f;
        axeHeadPos.y = newBody.partSprite.rect.height / newBody.partSprite.pixelsPerUnit * 0.75f;
        headRect.anchoredPosition = axeHeadPos;
    }

    public void SetHead(InventoryItem inventItem)
    {
        AxePart newHead = (AxePart)inventItem.itemData;
        headImage.sprite = newHead.partSprite;
        headImage.SetNativeSize();
    }
}
[System.Serializable]
public sealed class MultInfoPanel
{
    public GameObject multInfoPanel;
    public TextMeshProUGUI oldValueTxt;
    public TextMeshProUGUI newValueTxt;
}
