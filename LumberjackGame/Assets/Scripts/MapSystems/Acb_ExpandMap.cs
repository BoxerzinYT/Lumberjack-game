using UnityEngine;

public class Acb_ExpandMap : ActionBlock
{
    int myExpandCordX;
    public int MyExpandCordX { get { return myExpandCordX; } set { myExpandCordX = value; }}
    int myExpandCordY;
    public int MyExpandCordY { get { return myExpandCordY; } set { myExpandCordY = value; }}
    Map_GlobalMapManager mapGlobalMan;
    public Map_GlobalMapManager MapGlobalMan { set { mapGlobalMan = value;  } }

    public void ExpandMap()
    {
        if(Player.Hec_invent.hectorInventory.GetInventoryInventItem(Player.Hec_actSystem.ActionItemSelected).stackSize > 0)
        {
            mapGlobalMan.SpawnAIsland(myExpandCordX, myExpandCordY, Player.Hec_actSystem.ActionItemSelected.actionId);
            Player.Hec_invent.hectorInventory.RemoveItem(1, Player.Hec_actSystem.ActionInventItem);
            Player.Hec_actSystem.UpdateActionItemInHUD();
        }
    }
}
