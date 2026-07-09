using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RdOrDgnSettingsManager : MonoBehaviour
{
    [Header("ManualSettings")]
    [SerializeField] Transform comissionNpc; 
    [SerializeField] int mapId;
    //[SerializeField] RaidRewardsSo myRaidRewards;

    [Header("MainSettings")]
    [SerializeField] string[] scenesNames;
    [SerializeField] Sprite[] difficultIcons;
    [SerializeField] Image difficultIconPlace;
    [SerializeField] InventoryItemUIManager[] itensInScrollView;
    [SerializeField] ShowItensWithInventItemManager showItensWithInventUi;
    int difficultId;

    HectorInventory hjInvent;

    public void OnEnable()
    {
        SelectDifficult(0);
    }

    public void EntryRaidOrDungeon()
    {
        GameObject newGetInfoToRaid = new GameObject();
        newGetInfoToRaid.name = "RaidInfosFromOtherScene";
        GetInfosToRaid getInfo = newGetInfoToRaid.AddComponent<GetInfosToRaid>();
        getInfo.DifficultId = difficultId;
        getInfo.MapId = mapId;
        //getInfo.ComissionNpc = comissionNpc.position;
        DontDestroyOnLoad(newGetInfoToRaid);

        List<string> scenesToLoad = new List<string>();
        List<string> scenesToUnload = new List<string>();

        scenesToUnload.Add("World");
        scenesToLoad.Add(scenesNames[0]);
        //scenesToLoad.Add("MapleExpedition");
        EventsManager.eventM.GoToAnotherScene(scenesToLoad, scenesToUnload);
    }

    public void SelectDifficult(int value)
    {
        /*
        if (value != -1)
        {
            difficultId = value;
        }
        else { difficultId++; }

        if (difficultId >= myRaidRewards.dropsByDifficult.Length)
        {
            difficultId = 0;
        }

        difficultIconPlace.sprite = difficultIcons[difficultId];
        ShowTheRaidRewards();
        */
    }

    public void ShowTheRaidRewards()
    {
        /*
        foreach (var d in itensInScrollView) { d.gameObject.SetActive(false); }

        for (int i = 0; i < myRaidRewards.dropsByDifficult[difficultId].drops.Length; i++)
        {
            itensInScrollView[i].gameObject.SetActive(true);

            InventoryItemUIManager inventUi = showItensWithInventUi.ShowItemAndReturnIt(itensInScrollView[i].gameObject, myRaidRewards.dropsByDifficult[difficultId].drops[i].drop, 1);

            inventUi.StackTxt.text = (myRaidRewards.dropsByDifficult[difficultId].drops[i].chance * 100).ToString() + "%";
        }
        */
    }
}