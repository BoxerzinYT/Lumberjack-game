using UnityEngine;

public class Qol_BuySystem : MonoBehaviour
{

    public bool BuyWithCoins(Hec_Stats hecStats, float price)
    {
        if(HasCoins(hecStats.Hec_coins, price))
        {
            hecStats.Hec_coins -= price;
            return true;
        }
        else
        {
            EventsManager.eventM.CreateANot("You dont have enough coins!");
            return false;
        }
    }

    public bool BuyWithItensAndReturnOtherItem(Hec_Stats hecStats, InventoryItem priceItem, float price, InventoryItem addItem, float add)
    {
        if(HasItens(hecStats, priceItem) >= price)
        {
            hecStats.Hec_invent.hectorInventory.RemoveItem(price, priceItem);
            hecStats.Hec_invent.hectorInventory.AddItem(add, addItem);
            return true;
        }
        else
        {
            EventsManager.eventM.CreateANot("You dont have enough of that item!");
            return false;
        }
    }
    public bool BuyWithItens(Hec_Stats hecStats, InventoryItem priceItem, float price)
    {
        if(HasItens(hecStats, priceItem) >= price)
        {
            hecStats.Hec_invent.hectorInventory.RemoveItem(price, priceItem);
            return true;
        }
        else
        {
            EventsManager.eventM.CreateANot("You dont have enough of that item!");
            return false;
        }
    }

    public void CollectItem(Hec_Stats hecStats, InventoryItem item, float quant)
    {
        hecStats.Hec_invent.hectorInventory.AddItem(quant, item);
    }
    public void RemoveItem(Hec_Stats hecStats, InventoryItem item, float quant)
    {
        hecStats.Hec_invent.hectorInventory.RemoveItem(quant, item);
    }

    public bool HasCoins(float coins, float price)
    {
        return coins >= price;
    }

    public float HasItens(Hec_Stats hecStats, InventoryItem priceItem)
    {
        float stackSize = hecStats.Hec_invent.hectorInventory.GetInventoryItemQuant(priceItem);
        return stackSize;
    }
}
