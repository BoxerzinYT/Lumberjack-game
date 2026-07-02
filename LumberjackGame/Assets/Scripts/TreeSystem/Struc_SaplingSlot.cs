using System;
using UnityEngine;

public class Struc_SaplingSlot : ActionBlock
{
    [SerializeField] Breakable_Tree mapleTree_prefab_prot;
    public void AddSapling()
    {
        if(Player.MapleTree_saplings_prot > 0)
        {
            Player.MapleTree_saplings_prot -= 1;
            Breakable_Tree newMapleTree = Instantiate(mapleTree_prefab_prot);
            newMapleTree.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
