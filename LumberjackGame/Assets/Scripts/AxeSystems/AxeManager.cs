using System;
using System.Collections.Generic;
using UnityEngine;

public class AxeManager : MonoBehaviour
{
    [SerializeField] Hec_Stats hec_stats;
    [SerializeField] InventoryItem body;
    [SerializeField] InventoryItem head;
    public InventoryItem Body { get { return body; }}
    public InventoryItem Head { get { return head; }}

    [SerializeField] CustomizeAxeManager customizeMan;
    public CustomizeAxeManager CustomizeMan { get { return customizeMan; }}

    List<MultiplicatorSettings> AxeMults = new List<MultiplicatorSettings>();

    public void Start()
    {
        customizeMan.SetBody(body);
        customizeMan.SetHead(head);
        LoadAxeMults();
    }

    public void LoadAxeMults()
    {
        AxePart newBody = (AxePart)body.itemData;
        AxePart newHead = (AxePart)head.itemData;
        foreach(var m in newBody.mults)
        {
            hec_stats.AddOrRemoveMult(1, m, m.add * body.itemRank.rankMultiplier);
            AxeMults.Add(m);
        }
        foreach(var m in newHead.mults)
        {
            hec_stats.AddOrRemoveMult(1, m, m.add * head.itemRank.rankMultiplier);
            AxeMults.Add(m);
        }
    }

    public void SetNewBody(InventoryItem inventItem)
    {
        AxePart oldBody = (AxePart)body.itemData;
        AxePart newBody = (AxePart)inventItem.itemData;
        foreach(var m in oldBody.mults)
        {
            hec_stats.AddOrRemoveMult(-1, m, m.add * body.itemRank.rankMultiplier);
            AxeMults.Remove(m);
        }
        foreach(var m in newBody.mults)
        {
            hec_stats.AddOrRemoveMult(1, m, m.add * inventItem.itemRank.rankMultiplier);
            AxeMults.Add(m);
        }
        body = inventItem;
        customizeMan.SetBody(inventItem);
    }
    public void SetNewHead(InventoryItem inventItem)
    {
        AxePart oldHead = (AxePart)head.itemData;
        AxePart newHead = (AxePart)inventItem.itemData;
        foreach(var m in oldHead.mults)
        {
            hec_stats.AddOrRemoveMult(-1, m, m.add * head.itemRank.rankMultiplier);
            AxeMults.Remove(m);
        }
        foreach(var m in newHead.mults)
        {
            hec_stats.AddOrRemoveMult(1, m, m.add * inventItem.itemRank.rankMultiplier);
            AxeMults.Add(m);
        }
        head = inventItem;
        customizeMan.SetHead(inventItem);
    }
}
