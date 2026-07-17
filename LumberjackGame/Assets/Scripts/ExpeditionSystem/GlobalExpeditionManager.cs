using System.Collections;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalExpeditionManager : MonoBehaviour
{
    [Header("MainSettings")]
    CameraFollow cameraFollow;
    Hec_Stats hec_stats;
    Global_TimerSystem timerSystem;
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] StructureSystem goBackStruc;
    [SerializeField] GameObject youLostImage;
    [SerializeField] GameObject youWonImage;

    public Struc_GetInfoToExpedition getExpeditionInfo { get; private set; }
    Map_ExpeditionMapManager thisExpeditionMapMan;
    [SerializeField] Biome[] biomes;
    public Biome[] Biomes { get { return biomes; }}
    [SerializeField] Difficult[] difficults;
    [SerializeField] ExpeditionModes[] expeditionModes;
    int modeIdSelected;

    [Header("GeneralSettings")]
    [SerializeField] Expedition_GeneralSettings[] generalSettings;
    
    [Header("DropsSettings")]
    [SerializeField] Breakable_Chest chestPrefab;
    [SerializeField] Transform[] chestSpawnSquares;

    [Header("PanelSettings")]
    [SerializeField] UI_PanelManager beforeStartPanel;
    [SerializeField] TextMeshProUGUI modeTitleTxt;
    [SerializeField] TextMeshProUGUI modeDescriptionTxt;
    [SerializeField] TextMeshProUGUI biomeSelectedTxt;
    [SerializeField] Image biomeSelectedImage;
    [SerializeField] TextMeshProUGUI difficultSelectedTxt;
    [SerializeField] Image difficultSelectedImage;
    [SerializeField] Button startGameButton;

    [Header("ChopDownTreesSettings")]
    [SerializeField] Global_SpawnTrees global_spawnTrees;
    [SerializeField] TextMeshProUGUI treesDestroyedTxt;
    int treesDestroyed;

    [Header("ResultsScreen")]
    [SerializeField] UnityEvent whenOver;
    [SerializeField] Expedition_ResultsCalculation resulsCalc;

    public int allStunsSituations { get; set; }
    public int stunnedTimes { get; set; }
    public bool completedFirst { get; private set;}
    public bool completedSecond { get; private set; }

    public void Start()
    {
        goBackStruc.gameObject.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scene_ExpeditionManager"));
        cameraFollow = FindFirstObjectByType<CameraFollow>();
        hec_stats = FindFirstObjectByType<Hec_Stats>();
        getExpeditionInfo = FindFirstObjectByType<Struc_GetInfoToExpedition>();

        
        Invoke(nameof(LateStart), 1);
    }

    public void Update()
    {
        if(timerSystem != null)
        {
            timerTxt.text = timerSystem.UpdateTimer();
        }
    }

    public void LateStart()
    {
        EventsManager.eventM.playerCanInteract = false;
        EventsManager.eventM.playerCanWalk = false;
        thisExpeditionMapMan = FindFirstObjectByType<Map_ExpeditionMapManager>();
        cameraFollow.CallSetZoom(75, 1);
        Invoke(nameof(SetupPanel), 6);
        CalculateMode();
    }

    public void CalculateMode()
    {
        //int modeId = Random.Range(0, expeditionModes.Length);
        int modeId = 0;
        modeIdSelected = modeId;

        if(modeId == 0)
        {
            ChopDownTreesSelected();
        }
    }

    public void SetupPanel()
    {
        cameraFollow.CallSetZoom(25, 1);
        modeTitleTxt.text = expeditionModes[modeIdSelected].ModeName;
        modeDescriptionTxt.text = expeditionModes[modeIdSelected].ModeDescription;

        biomeSelectedTxt.text = biomes[getExpeditionInfo.BiomeId].biomeName;
        biomeSelectedImage.sprite = biomes[getExpeditionInfo.BiomeId].biomeIcon;

        difficultSelectedTxt.text = difficults[getExpeditionInfo.DifficultId].difficultName;
        difficultSelectedImage.sprite = difficults[getExpeditionInfo.DifficultId].difficultIcon;

        startGameButton.onClick = new Button.ButtonClickedEvent();
        startGameButton.onClick.AddListener(() => EventsManager.eventM.playerCanInteract = true);
        startGameButton.onClick.AddListener(() => EventsManager.eventM.playerCanWalk = true);
        startGameButton.onClick.AddListener(() => beforeStartPanel.NormalClose());
        if(modeIdSelected == 0)
        {
            startGameButton.onClick.AddListener(() => ChopDownTrees_Start());
        }
        else
        {
            
        }

        beforeStartPanel.Open();

    }

    public void ChopDownTreesSelected()
    {
        Invoke(nameof(CallSpawnTrees), 2);
        treesDestroyedTxt.text = "Trees destroyed: " + treesDestroyed.ToString() + "/" + 
        generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].treesQuant.ToString();
        timerSystem = 
        EventsManager.eventM.CreateTimer
        (generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].timeToComplete, 
        () => CalculateResults(), false);
    }

    public void CallSpawnTrees()
    {
        StartCoroutine(global_spawnTrees.SpawnTrees((int)generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].treesQuant,
        generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].trees,
        generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].treeRankVariation,
        getExpeditionInfo.ExpeditionSceneName, thisExpeditionMapMan.MySpawnSquares));
    }

    public void ChopDownTrees_Start()
    {
        timerSystem.StartTimer();
    }

    public void CalculateResults()
    {
        whenOver?.Invoke();
        if(treesDestroyed < generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].firstQuant)
        {
            ChopDownTrees_Lost();
        }
        else if(treesDestroyed >= generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].firstQuant)
        {
            ChopDownTrees_Won();
        }
    }

    public void ChopDownTrees_Lost()
    {
        StartCoroutine(GlobalLost());
    }

    public void ChopDownTrees_Won()
    {
        StartCoroutine(GlobalWon());
    }

    public void ChopDownTrees_DestroyedATree()
    {
        treesDestroyed++;
        if(treesDestroyed < generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].firstQuant)
        {
            treesDestroyedTxt.text = "Trees destroyed: " + treesDestroyed.ToString() + "/" + generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].firstQuant.ToString();
            treesDestroyedTxt.color = Color.white;
        }
        else if(treesDestroyed < generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].secondQuant)
        {
            treesDestroyedTxt.text = "Trees destroyed: " + treesDestroyed.ToString() + "/" + generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].secondQuant.ToString();
            treesDestroyedTxt.color = Color.green;
            completedFirst = true;
        }
        else if(treesDestroyed >= generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].secondQuant)
        {
            treesDestroyedTxt.text = "Trees destroyed: " + treesDestroyed.ToString() + "/" 
            + generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].secondQuant.ToString();
            treesDestroyedTxt.color = Color.green;
            completedSecond = true;
            if(treesDestroyed >= generalSettings[getExpeditionInfo.BiomeId].cdt_settings[getExpeditionInfo.DifficultId].treesQuant)
            {
                ChopDownTrees_Won();
            }
        }
    }

    public void FinishSettings()
    {
        foreach(var t in global_spawnTrees.TreesSaved)
        {
            t.WhenDie.AddListener(() => ChopDownTrees_DestroyedATree());
        }
    }

    public IEnumerator GlobalWon()
    {
        EventsManager.eventM.playerCanInteract = false;
        EventsManager.eventM.playerCanWalk = false;

        youWonImage.SetActive(true);
        goBackStruc.gameObject.SetActive(true);
        timerSystem.CanRun = false;

        hec_stats.Hec_anim.SetBool("Won", true);
        yield return new WaitForSeconds(2);

        youWonImage.SetActive(false);

        StartCoroutine(resulsCalc.ShowResults(modeIdSelected, getExpeditionInfo.BiomeId, true));


        
        for(int i=0; i<generalSettings[getExpeditionInfo.BiomeId].chests_settings[getExpeditionInfo.DifficultId].chestQuantToSpawn; i++)
        {
            Breakable_Chest newChest = Instantiate(chestPrefab);
            bool isInMap = false;
            bool touchingOtherBobj = true;
            bool touchingStructure = true;
            while (!isInMap || touchingOtherBobj || touchingStructure)
            {
                newChest.transform.position = new Vector2
                (Random.Range(chestSpawnSquares[0].position.x, chestSpawnSquares[1].position.x),
                Random.Range(chestSpawnSquares[0].position.y, chestSpawnSquares[1].position.y));
                isInMap = newChest.AmIinMap();
                touchingOtherBobj = newChest.TouchingAnotherBreakableObject();
                touchingStructure = newChest.TouchingStructure();
                yield return new WaitForSeconds(0.01f);
            }

            float chance = Random.Range(0f,1f);

            foreach(var c in generalSettings[getExpeditionInfo.BiomeId].chests_settings[getExpeditionInfo.DifficultId].chests)
            {
                if(chance <= c.chance)
                {
                    newChest.Chest = c.chest;
                    newChest.SetChest();
                    break;
                }
            }
            SceneManager.MoveGameObjectToScene(newChest.gameObject, SceneManager.GetSceneByName(SceneManager.GetActiveScene().name));
            yield return new WaitForSeconds(0.01f);
        }

        StopCoroutine("GlobalWin");
    }

    public IEnumerator GlobalLost()
    {
        EventsManager.eventM.playerCanInteract = false;
        EventsManager.eventM.playerCanWalk = false;

        hec_stats.Hec_anim.SetBool("Lost", true);
        yield return new WaitForSeconds(1.2f);
        youLostImage.SetActive(true);

        yield return new WaitForSeconds(2);

        StartCoroutine(resulsCalc.ShowResults(modeIdSelected, getExpeditionInfo.BiomeId, false));

        youLostImage.SetActive(false);

        yield return new WaitForSeconds(1);

        hec_stats.Hec_anim.SetBool("Lost", false);

        StopCoroutine("GlobalLost");
    }

    public void GoBack()
    {
        EventsManager.eventM.AddScenes(SceneManager.GetActiveScene().name, 1);
        EventsManager.eventM.AddScenes("Scene_Expedition_Biome" + getExpeditionInfo.BiomeId, 1);

        EventsManager.eventM.AddScenes("World", 0);
        
        EventsManager.eventM.GoToAnotherScene();
        Destroy(getExpeditionInfo.gameObject);
    }
}
[System.Serializable]
public sealed class ExpeditionModes
{
    public string ModeName;
    [TextArea]
    public string ModeDescription;

    public int ModeId;
}
[System.Serializable]
public sealed class Expedition_GeneralSettings
{
    public string biomeName;
    public ChopDownTrees_DifficultSettings[] cdt_settings;
    public ExpeditionChestsDifficultController[] chests_settings;
}
[System.Serializable]
public sealed class ExpeditionChestsDifficultController
{
    public string difficultName;
    public Expedition_ChestWithChance[] chests;
    public int chestQuantToSpawn;
}
[System.Serializable]
public sealed class Expedition_ChestWithChance
{
    public Chest chest;
    public float chance;
}
[System.Serializable]
public sealed class ChopDownTrees_DifficultSettings
{
    public string difficultName;
    public ChopDownTrees_TreesVariationsSettings[] trees;
    public ChopDownTrees_TreesRankVariations[] treeRankVariation;
    public float timeToComplete;
    public float treesQuant;
    public int firstQuant; //0,5 star
    public int secondQuant; //other 0,5
}
[System.Serializable]
public sealed class ChopDownTrees_TreesVariationsSettings
{
    public Sappling tree;
    public float chance;
}
[System.Serializable]
public sealed class ChopDownTrees_TreesRankVariations
{
    public Rank treeRank;
    public float chance;
}
