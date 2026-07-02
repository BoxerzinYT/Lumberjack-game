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
        /*
        if(Player.MapleBiome_expansionScrolls_prot > 0)
        {

            Player.MapleBiome_expansionScrolls_prot -= 1;
        }
        */

        mapGlobalMan.SpawnAIsland(myExpandCordX, myExpandCordY);
    }
}
