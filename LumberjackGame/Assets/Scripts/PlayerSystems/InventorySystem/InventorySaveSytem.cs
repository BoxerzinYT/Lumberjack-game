using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item database", menuName = "Inventory/Items/DataBase")]
public class InventorySaveSytem : ScriptableObject, ISerializationCallbackReceiver
{
    public Item[] items;
    public Rank[] ranks;
    public Dictionary<Item, int> GetId = new Dictionary<Item, int>();
    public Dictionary<int, Item> GetItem = new Dictionary<int, Item>();
    public Dictionary<int, Rank> GetRank = new Dictionary<int, Rank>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<Item, int>();
        GetItem = new Dictionary<int, Item>();
        GetRank = new Dictionary<int, Rank>();
        for(int i = 0; i < items.Length; i++)
        {
            GetId.Add(items[i], i);
            GetItem.Add(i, items[i]);
        }
        for(int i=0; i<ranks.Length; i++)
        {
            GetRank.Add(i, ranks[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
