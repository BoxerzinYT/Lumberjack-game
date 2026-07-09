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
        if(Player.Hec_actSystem.ActionItemSelected.actionId == myActionId)
        {
            if(BuyWithItens(Player, Player.Hec_actSystem.ActionInventItem.itemData, 1))
            {
                Player.Hec_actSystem.UpdateActionItemInHUD();
                Breakable_Tree newMapleTree = Instantiate(mapleTree_prefab_prot);
                newMapleTree.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
        }
    }
}
