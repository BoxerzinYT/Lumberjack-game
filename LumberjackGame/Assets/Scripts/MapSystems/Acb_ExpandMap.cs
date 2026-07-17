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
        if(BuyWithItens(Player, Player.Hec_actSystem.ActionInventItem, 1))
        {
            mapGlobalMan.SpawnAIsland(myExpandCordX, myExpandCordY, Player.Hec_actSystem.ActionItemSelected.actionId);
            Player.Hec_actSystem.UpdateActionItemInHUD();
        }
    }
}
