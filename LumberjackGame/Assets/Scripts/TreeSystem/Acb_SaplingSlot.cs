using System;
using UnityEngine;

public class Acb_SaplingSlot : ActionBlock
{
    [SerializeField] Breakable_Tree mapleTree_prefab_prot;
    [SerializeField] int myActionId;

    public void SetMyActionId(int _myActionId)
    {
        myActionId = _myActionId;
    }

    public void AddSapling()
    {
        if(Player.Hec_invent.hectorInventory.GetInventoryInventItem(Player.Hec_actSystem.ActionItemSelected).stackSize > 0
        && Player.Hec_actSystem.ActionItemSelected.actionId == myActionId)
        {
            Breakable_Tree newMapleTree = Instantiate(mapleTree_prefab_prot);
            newMapleTree.transform.position = this.transform.position;
            Player.Hec_invent.hectorInventory.RemoveItem(1, Player.Hec_actSystem.ActionInventItem);
            Player.Hec_actSystem.UpdateActionItemInHUD();
            Destroy(this.gameObject);
        }
    }
}
