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

    public bool BuyWithItensAndReturnOtherItem(Hec_Stats hecStats, Item price_item, float price, Item add_item, float add)
    {
        if(HasItens(hecStats, price_item) >= price)
        {
            hecStats.Hec_invent.hectorInventory.RemoveItem(price, new InventoryItem(hecStats.Hec_invent.hectorInventory.GetIDOfAItem(price_item), price_item, HasItens(hecStats, price_item)));
            hecStats.Hec_invent.hectorInventory.AddItem(add, new InventoryItem(hecStats.Hec_invent.hectorInventory.GetIDOfAItem(add_item), add_item, 0));
            return true;
        }
        else
        {
            EventsManager.eventM.CreateANot("You dont have enough of that item!");
            return false;
        }
    }
    public bool BuyWithItens(Hec_Stats hecStats, Item price_item, float price)
    {
        if(HasItens(hecStats, price_item) >= price)
        {
            hecStats.Hec_invent.hectorInventory.RemoveItem(price, new InventoryItem(hecStats.Hec_invent.hectorInventory.GetIDOfAItem(price_item), price_item, HasItens(hecStats, price_item)));
            return true;
        }
        else
        {
            EventsManager.eventM.CreateANot("You dont have enough of that item!");
            return false;
        }
    }

    public bool HasCoins(float coins, float price)
    {
        return coins >= price;
    }

    public float HasItens(Hec_Stats hecStats, Item price_item)
    {
        if(hecStats.Hec_invent.hectorInventory.GetItemDataQuant(price_item) > 0)
        {
            return hecStats.Hec_invent.hectorInventory.GetItemDataQuant(price_item);
        }

        return 0;
    }
}
