using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfoUIManager : MonoBehaviour
{
    [SerializeField] UI_PanelManager panelMan;
    [SerializeField] TextMeshProUGUI titleTxt;
    [SerializeField] TextMeshProUGUI rarityTxt;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemDescription;
    [Header("ToolUI")]
    [SerializeField] GameObject toolDamUI;
    [SerializeField] TextMeshProUGUI toolDamageTxt;

    [Header("SellUI")]
    [SerializeField] GameObject SellPriceUI;
    [SerializeField] TextMeshProUGUI sellPriceTxt;

    [Header("Buttons")]
    [SerializeField] Button equipButton;
    [SerializeField] Button useButton;
    [SerializeField] Button openButton;

    [Header("Egg")]
    [SerializeField] GameObject petsInEggPanel;

    public void DisplayInfo(InventoryItem item)
    {
        panelMan.Open();

        titleTxt.text = item.itemData.itemName;
        rarityTxt.text = item.itemData.itemRarity.rarityName;
        rarityTxt.color = item.itemData.itemRarity.rarityColor;
        //trackingBg.color = item.itemData.rarity.rarityColor;
        itemImage.sprite = item.itemData.itemIcon;
        itemDescription.text = item.itemData.itemDescription;

        //UpdateEnchantsUI(item);

        //Is Tool
        /*
        if(item.itemData.GetType() == typeof(Tools))
        {
            Tools t = (Tools)item.itemData;

            toolDamUI.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            EventsManager.eventM.UpdateVariables(t.damage);
        }
        else { toolDamUI.gameObject.SetActive(false); equipButton.gameObject.SetActive(false); }
        */
        if (item.itemData.GetType() == typeof(Sell_Item))
        {
            Sell_Item m = (Sell_Item)item.itemData;

            SellPriceUI.gameObject.SetActive(true);
            //EventsManager.eventM.UpdateVariables(m.quant);
        }
        else { SellPriceUI.gameObject.SetActive(false); }

        /*
        if (item.itemData.GetType() == typeof(Egg))
        {
            Egg m = (Egg)item.itemData;

            petsInEggPanel.gameObject.SetActive(true);
        }
        else { petsInEggPanel.gameObject.SetActive(false);}
        */
        //popupCanvasObject.SetActive(true);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }
}
