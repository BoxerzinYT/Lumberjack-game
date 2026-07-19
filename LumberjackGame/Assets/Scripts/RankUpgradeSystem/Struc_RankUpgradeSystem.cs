using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Struc_RankUpgradeSystem : StructureSystem
{
    [SerializeField] ShowInventoryItens showInventItens;

    [Header("MainSettings")]
    [SerializeField] Canvas myHUD;
    [SerializeField] GameObject upgradeRankPanel;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemQuantTxt;
    [SerializeField] Image actualRank;
    [SerializeField] Image nextRank;
    [SerializeField] Image biomeCoinImage;
    [SerializeField] TextMeshProUGUI biomeCoinTxt;
    [SerializeField] Button cancelButton;
    [SerializeField] Button upgradeButton;
    [SerializeField] SelectQuant selectQuantPanel;

    [Header("Values")]
    [SerializeField] InventoryItem[] biomeCoins;
    [SerializeField] Rank[] ranks;
    [SerializeField] float[] commonValues;
    [SerializeField] float[] rareValues;
    [SerializeField] float[] megaValues;
    [SerializeField] float[] epicValues;
    [SerializeField] float[] legendValues;
    [SerializeField] float[] galaxyValues;

    public void WhenOpen()
    {
        upgradeRankPanel.SetActive(false);
        showInventItens.ShowItensInUI(LastPlayerThatPassHere.Hec_invent.hectorInventory.inventory);
    }

    public void SelectQuantForUpgrade(InventoryItem inventItem, GameObject inSellObj, InventoryItemUIManager itemPrefabed)
    {
        SelectQuant newSQUI = Instantiate(selectQuantPanel, myHUD.transform);

        newSQUI.SelectButton.onClick.AddListener(() => SelectedQuantForUpgrade(inventItem, inSellObj, itemPrefabed, newSQUI.GetValue()));
        newSQUI.Slider.minValue = 1;
        newSQUI.Slider.maxValue = inventItem.stackSize;
    }

    public void SelectedQuantForUpgrade(InventoryItem inventItem, GameObject inSellObj, InventoryItemUIManager itemPrefabed, float quantSelected)
    {
        upgradeRankPanel.SetActive(true);
        inSellObj.SetActive(true);

        itemImage.sprite = inventItem.itemData.itemIcon;
        itemQuantTxt.text = EventsManager.eventM.UpdateVariables(quantSelected);

        actualRank.sprite = inventItem.itemRank.rankIcon;
        nextRank.sprite = ranks[inventItem.itemRank.rankId + 1].rankIcon;

        biomeCoinImage.sprite = biomeCoins[inventItem.itemData.itemBiome.biomeId].itemData.itemIcon;

        float priceToUpdateRank = 0;
        if(inventItem.itemData.itemRarity.rarityId == 0)
        {
            priceToUpdateRank = commonValues[inventItem.itemRank.rankId];
        }
        else if(inventItem.itemData.itemRarity.rarityId == 1)
        {
            priceToUpdateRank = rareValues[inventItem.itemRank.rankId];
        }
        else if(inventItem.itemData.itemRarity.rarityId == 2)
        {
            priceToUpdateRank = megaValues[inventItem.itemRank.rankId];
        }
        else if(inventItem.itemData.itemRarity.rarityId == 3)
        {
            priceToUpdateRank = epicValues[inventItem.itemRank.rankId];
        }
        else if(inventItem.itemData.itemRarity.rarityId == 4)
        {
            priceToUpdateRank = legendValues[inventItem.itemRank.rankId];
        }
        else if(inventItem.itemData.itemRarity.rarityId == 5)
        {
            priceToUpdateRank = galaxyValues[inventItem.itemRank.rankId];
        }


        biomeCoinTxt.text = EventsManager.eventM.UpdateVariables(priceToUpdateRank * quantSelected);
        upgradeButton.onClick = new Button.ButtonClickedEvent();
        cancelButton.onClick = new Button.ButtonClickedEvent();
        upgradeButton.onClick.AddListener(() => Upgrade(inventItem, quantSelected, priceToUpdateRank));
        cancelButton.onClick.AddListener(() => DeselectQuantForUpgrade(inSellObj));
    }

    public void DeselectQuantForUpgrade(GameObject inSellObj)
    {
        inSellObj.gameObject.SetActive(false);
        upgradeRankPanel.SetActive(false);
    }

    public void Upgrade(InventoryItem inventItem, float quantSelected, float priceToUpdateRank)
    {
        if(HasItens(LastPlayerThatPassHere, biomeCoins[inventItem.itemData.itemBiome.biomeId]) >= priceToUpdateRank * quantSelected)
        {
            RemoveItem(LastPlayerThatPassHere, biomeCoins[inventItem.itemData.itemBiome.biomeId], priceToUpdateRank * quantSelected);
            CollectItem(LastPlayerThatPassHere, new InventoryItem(inventItem.itemData, ranks[inventItem.itemRank.rankId + 1], 0), quantSelected);

            RemoveItem(LastPlayerThatPassHere, inventItem, (int)quantSelected);
            EventsManager.eventM.CreateANot("Upgraded the " + inventItem.itemData.itemName + " to rank " + ranks[inventItem.itemRank.rankId + 1].rankName + "!");
            WhenOpen();
        }
        else
        {
            return;
        }
    }

    public void FinishSettings()
    {
        foreach(var s in showInventItens.ItensPrefabed)
        {
            if(s.inventItem.itemRank.rankId >= 7 || !s.inventItem.itemData.canChangeMyRank) { s.ItemPrefab.gameObject.SetActive(false); continue; }
            GameObject inSellGob = s.ItemPrefab.transform.Find("sp").transform.Find("SellSelected").gameObject;
            s.ItemPrefab.MyButton.onClick = new Button.ButtonClickedEvent();
            if(s.inventItem.stackSize > 1)
            {
                s.ItemPrefab.MyButton.onClick.AddListener(() => SelectQuantForUpgrade(s.inventItem, inSellGob, s.ItemPrefab));
            }
            else if(s.inventItem.stackSize <= 1)
            {
                s.ItemPrefab.MyButton.onClick.AddListener(() => SelectedQuantForUpgrade(s.inventItem, inSellGob, s.ItemPrefab, 1));
            }
        }
    }
}
