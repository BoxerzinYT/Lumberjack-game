using System;
using UnityEngine;

public class Struc_SaplingSlot : StructureSystem
{
    [SerializeField] Breakable_Tree mapleTree_prefab_prot;
    public void AddSapling()
    {
        if(LastPlayerThatPassHere.MapleTree_saplings_prot > 0)
        {
            LastPlayerThatPassHere.MapleTree_saplings_prot -= 1;
            Breakable_Tree newMapleTree = Instantiate(mapleTree_prefab_prot);
            newMapleTree.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
