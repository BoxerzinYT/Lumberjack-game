using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new Inventory", menuName = "Inventory/new invent")]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public List<InventoryItem> inventory;
    public string savepath;
    InventorySaveSytem database;
    public InventorySaveSytem Database { get { return database; } }

    void OnEnable()
    {
#if UNITY_EDITOR
        database = (InventorySaveSytem)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(InventorySaveSytem));
#else
        database = Resources.Load<InventorySaveSytem>("Database");
#endif
        inventory.Clear();
        LoadInvent();

    }

    public InventoryItem GetInventoryInventItem(Item obj)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemData == obj)
            {
                return inventory[i];
            }
        }

        return null;
    }

    public int GetInventoryItemQuant(Item obj)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemData == obj)
            {
                return inventory[i].stackSize;
            }
        }

        return 0;
    }

    public int GetIDOfAItem(Item obj)
    {
        for(int i=0; i < database.items.Length; i++)
        {
            if (database.items[i] == obj) {  return i; }
        }
        return -1;
    }

    public void AddItem(int amount, InventoryItem inventItem)
    {
        for (int i=0; i < inventory.Count; i++)
        {
            if(!inventItem.itemData.isStackable) { break; }


            if(inventory[i].itemData == inventItem.itemData)
            {
                inventory[i].AddToStack(amount);
                SaveInvent();
                return;
            }
        }

        if (inventItem != null) { inventory.Add(inventItem); SaveInvent(); }
    }

    public void RemoveItem(int amount, InventoryItem inventItem)
    {
        for (int i=0; i < inventory.Count; i++)
        {
            if(inventory[i].itemData == inventItem.itemData && inventItem.itemData.isStackable)
            {
                inventory[i].RemoveFromStack(amount);
                if(inventory[i].stackSize <= 0)
                {
                    inventory.Remove(inventory[i]);
                }
                SaveInvent();
                return;
            }
            else if (!inventItem.itemData.isStackable)
            {
                break;
            }
        }

        if (inventItem != null) { inventory.Remove(inventItem); SaveInvent(); }
    }

    /*
    public void AddItemWithEnchant(int amount, InventoryItem inventItem)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].enchants == inventItem.enchants)
            {
                inventory[i].AddToStack(amount);
                SaveInvent();
                return;
            }
        }

        inventory.Add(inventItem);
        SaveInvent();
    }
    */

    /*
    public void AddItem(int amount, InventoryItem inventItem)
    {
        amount = inventItem.stackSize;

        for (int i=0; i < inventory.Count; i++)
        {
            if (inventItem.itemData.isStackable == false)
            {
                break;
            }


            if (inventory[i].itemData == inventItem.itemData) //Mesmos itens
            {
                if (inventItem.enchants.Count > 0) //O item novo tem encantamentos?
                {
                    //Debug.Log("tem encantamentos!");

                    if (inventory[i].enchants.Count > 0) //O item atual tem encantamentos?
                    {
                        //Debug.Log("o item a ser comparado tem encantamentos tambem!");
                        //Debug.Log("Count of enchants:" + inventory[i].enchants.Count);
                        //Debug.Log("Count of enchants of new item:" + inventItem.enchants.Count);

                        int sameEnchants = 0;
                        for (int e = 0; e < inventory[i].enchants.Count; e++)
                        {
                            //Debug.Log(e);

                            if (inventory[i].enchants[e].enchantId == inventItem.enchants[e].enchantId) //Mesmo encantamento?
                            {
                                sameEnchants++;
                                //Debug.Log(sameEnchants);
                                if (sameEnchants == inventItem.enchants.Count) //Ambos tem o mesmo encantamento?
                                {
                                   // Debug.Log("O item � identico ao ser comparado at� nos encantamentos!");
                                    inventory[i].AddToStack(amount);
                                    SaveInvent();
                                    return;
                                }
                            }
                            else //N�o tem os mesmos encantamentos
                            {
                                //Debug.Log("N�o s�o iguais em encantamentos");
                                break;
                            }
                        }
                    }
                    else //O item atual n�o tem encantamentos, prossiga para o pr�ximo
                    {
                        //Debug.Log("O item a ser comparado n�o tem encantamentos");
                        continue;
                    }
                }
                else //N�o tem encantamentos
                {
                    inventory[i].AddToStack(amount);
                   // Debug.Log("dont enchanted");
                    return;
                }
            }
        }
        if (inventItem.itemData.GetType() == typeof(BreakableItem) || inventItem.itemData.GetType() == typeof(Tools))
        {
            BreakableItem b = (BreakableItem)inventItem.itemData;
            inventItem.canBreak = b.canBreak;
            if (b.canBreak == true)
            {
                inventItem.quantToBreak = b.quantToBreak;
            }
        }

        if (inventItem != null) { inventory.Add(inventItem); }
        SaveInvent();
    }
    
    

    public void Remove(InventoryItem inventItem, int amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemData == inventItem.itemData) //Mesmos itens
            {
                if (inventItem.enchants.Count > 0) //O item novo tem encantamentos?
                {
                    //Debug.Log("tem encantamentos!");

                    if (inventory[i].enchants.Count > 0) //O item atual tem encantamentos?
                    {
                        //Debug.Log("o item a ser comparado tem encantamentos tambem!");
                        //Debug.Log("Count of enchants:" + inventory[i].enchants.Count);
                        //Debug.Log("Count of enchants of new item:" + inventItem.enchants.Count);

                        int sameEnchants = 0;
                        for (int e = 0; e < inventory[i].enchants.Count; e++)
                        {
                            //Debug.Log(e);

                            if (inventory[i].enchants[e].enchantId == inventItem.enchants[e].enchantId) //Mesmo encantamento?
                            {
                                sameEnchants++;
                                //Debug.Log(sameEnchants);
                                if (sameEnchants == inventItem.enchants.Count) //Ambos tem o mesmo encantamento?
                                {
                                    // Debug.Log("O item � identico ao ser comparado at� nos encantamentos!");
                                    inventory[i].RemoveFromStack(amount);
                                    if (inventory[i].stackSize <= 0) { inventory.Remove(inventItem); }
                                    SaveInvent();
                                    return;
                                }
                            }
                            else //N�o tem os mesmos encantamentos
                            {
                                //Debug.Log("N�o s�o iguais em encantamentos");
                                break;
                            }
                        }
                    }
                    else //O item atual n�o tem encantamentos, prossiga para o pr�ximo
                    {
                        //Debug.Log("O item a ser comparado n�o tem encantamentos");
                        continue;
                    }
                }
                else //N�o tem encantamentos
                {
                    inventory[i].RemoveFromStack(amount);
                    if (inventory[i].stackSize <= 0) { inventory.Remove(inventItem); }
                    SaveInvent();
                    // Debug.Log("dont enchanted");
                    return;
                }
            }
        }
    }
    */

    public void SaveInvent()
    {
        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savepath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    public void LoadInvent()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savepath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savepath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].itemData = database.GetItem[inventory[i].ID];
        }
    }
}
