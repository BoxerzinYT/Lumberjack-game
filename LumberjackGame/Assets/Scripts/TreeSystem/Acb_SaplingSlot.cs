using UnityEngine;

public class Acb_SaplingSlot : ActionBlock
{
    [SerializeField] int myActionId;

    public void SetMyActionId(int _myActionId)
    {
        myActionId = _myActionId;
    }

    public void AddSapling()
    {
        if(Player.Hec_actSystem.ActionItemSelected.actionId == myActionId)
        {
            if(BuyWithItens(Player, Player.Hec_actSystem.ActionInventItem, 1))
            {
                Sappling sappling = (Sappling)Player.Hec_actSystem.ActionInventItem.itemData;
                Player.Hec_actSystem.UpdateActionItemInHUD();
                Breakable_Tree newTree = Instantiate(sappling.myTree);
                newTree.transform.position = this.transform.position;
                newTree.AddSappling(Player.Hec_actSystem.ActionInventItem);
                Destroy(this.gameObject);
            }
        }
    }
}
