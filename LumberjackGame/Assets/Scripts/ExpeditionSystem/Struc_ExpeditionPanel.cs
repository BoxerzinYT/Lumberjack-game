using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Struc_ExpeditionPanel : StructureSystem
{
    [SerializeField] Map_GlobalMapManager mapGlobalMan;
    [SerializeField] Struc_GetInfoToExpedition getInfoToExPrefab;

    [SerializeField] UI_ItemWithTxtPanel[] biomesPanel;
    [SerializeField] UI_ItemWithTxtPanel[] difficultsPanel;

    [SerializeField] GameObject biomeArea;
    [SerializeField] GameObject difficultiesArea;
    [SerializeField] GameObject buttonArea;

    [SerializeField] GameObject nextPhase;

    [Header("RandomGeneration")]
    [SerializeField] Toggle randomBiomesToggle;
    [SerializeField] Toggle randomDifficultsToggle;
    bool randomBiomeMode;
    bool randomDifficultMode;
    [SerializeField] List<int> randomGenerateBiomesId;
    [SerializeField] List<int> randomDifficultsId;

    [Header("ButtonArea")]
    [SerializeField] TextMeshProUGUI coinsTxt;
    float price;

    int phase; //0=BiomeSelection, 1=DifficultSelection, 2=StartSelection
    int biomeId;
    int difficultId;

    public void WhenOpenPanel()
    {
        biomeId = -1;
        difficultId = -1;
        phase = 0;

        randomBiomeMode = false;
        randomBiomesToggle.isOn = false;
        randomDifficultMode = false;
        randomDifficultsToggle.isOn = false;

        randomGenerateBiomesId.Clear();

        //ResetDifficultiesPanel();
        UpdateBiomesPanel();
        UpdatePhaseSelection();
    }

    public void ResetDifficultiesPanel()
    {
        foreach(var d in difficultsPanel)
        {
            d.SelectedBg.SetActive(false);
            d.LockerBg.SetActive(false);
        }
    }

    public void UpdateBiomesPanel()
    {
        //Biomes first
        int activateRandom = 0; //quando essa variavel for igual a lenght dos biomas, o modo random vai
        //ativar sozinho
        for(int i=0; i<biomesPanel.Length; i++)
        {
            //biomesPanel[i].MyButton.onClick = new Button.ButtonClickedEvent();
            //biomesPanel[i].MyButton.onClick.AddListener(() => SelectedBiome());
            biomesPanel[i].SelectedBg.SetActive(false);

            if(mapGlobalMan.Map_HowMuchEachBiome.GetBiomeId(i) >=
            mapGlobalMan.Biomes[i].quantOf_toUnlockSelection)
            {
                biomesPanel[i].LockerBg.SetActive(false);
            }
            else if(mapGlobalMan.Map_HowMuchEachBiome.GetBiomeId(i) >=
            mapGlobalMan.Biomes[i].quantOf_toUnlockOnRandomGeneration)
            {
                activateRandom++;
                biomesPanel[i].LockerBg.SetActive(true);
            }
            else
            {
                biomesPanel[i].LockerBg.SetActive(true);
            }
        }

        if(activateRandom >= biomesPanel.Length)
        {
            SetRandomBiomeMode(true);
            randomBiomesToggle.gameObject.SetActive(false);
        }
        else
        {
            randomBiomesToggle.gameObject.SetActive(true);
        }
        
    }

    public void SetRandomBiomeMode(bool set)
    {
        randomGenerateBiomesId.Clear();
        randomBiomeMode = set;
        if (randomBiomeMode)
        {
            for(int i=0; i<biomesPanel.Length; i++)
            {
                randomGenerateBiomesId.Add(i);
                biomesPanel[i].LockerBg.SetActive(true);
                biomesPanel[i].LockerButton.onClick.AddListener(() => 
                EventsManager.eventM.CreateANot("Your island isnt upgraded enough to allow you select this..."));
            }
            UpdateDifficultsPanel();
            nextPhase.SetActive(true);
        }
        else if(!randomBiomeMode)
        {
            for(int i=0; i<biomesPanel.Length; i++)
            {
                biomesPanel[i].LockerBg.SetActive(false);
            }
            nextPhase.SetActive(false);
        }
    }

    public void UpdateDifficults()
    {
        if(biomeId == -1) { return; }
        if(mapGlobalMan.Map_HowMuchEachBiome.GetBiomeId(biomeId) >=
        mapGlobalMan.Biomes[biomeId].quantOf_toUnlockDifficultSelection)
        {
            foreach(var d in difficultsPanel)
            {
                d.LockerBg.SetActive(false);
            }
        }
        else
        {
            randomDifficultsToggle.gameObject.SetActive(false);
            SetRandomDifficulties(true);
        }
    }

    public void UpdateDifficultsPanel()
    {
        if(biomeId == -1)
        {
            randomDifficultsToggle.gameObject.SetActive(false);
            SetRandomDifficulties(true);
            return;
        }
        else if(difficultId != -1)
        {
            nextPhase.SetActive(true);
        }
        else { nextPhase.SetActive(false); }
    }

    public void SetRandomDifficulties(bool set)
    {
        randomDifficultMode = set;
        if (randomDifficultMode)
        {
            foreach(var d in difficultsPanel)
            {
                d.LockerBg.SetActive(true);
            }
            
            nextPhase.SetActive(true);
        }
        else if(!randomDifficultMode)
        {
            foreach(var d in difficultsPanel)
            {
                d.LockerBg.SetActive(false);
            }

            nextPhase.SetActive(false);
        }
    }

    public void SelectedBiome(int _biomeId)
    {
        if(biomeId == -1)
        {
            biomeId = _biomeId;
            biomesPanel[_biomeId].SelectedBg.SetActive(true);
            randomBiomesToggle.gameObject.SetActive(false);
            nextPhase.SetActive(true);
        }
        else if(biomeId == _biomeId)
        {
            biomeId = -1;
            biomesPanel[_biomeId].SelectedBg.SetActive(false);
            randomBiomesToggle.gameObject.SetActive(true);
            nextPhase.SetActive(false);
        }
        UpdateDifficults();
    }

    public void SelectedDifficult(int _difficultId)
    {
        if(difficultId == -1)
        {
            difficultId = _difficultId;
            difficultsPanel[_difficultId].SelectedBg.SetActive(true);
            randomDifficultsToggle.gameObject.SetActive(false);
            nextPhase.SetActive(true);
        }
        else if(difficultId == _difficultId)
        {
            difficultId = -1;
            difficultsPanel[_difficultId].SelectedBg.SetActive(false);
            randomDifficultsToggle.gameObject.SetActive(true);
            nextPhase.SetActive(false);
        }
    }

    public void UpdateButtonArea()
    {
        price = 100;
        coinsTxt.text = EventsManager.eventM.UpdateVariables(price);
    }

    public void StartExpedition()
    {
        if(BuyWithCoins(LastPlayerThatPassHere, price))
        {
            EventsManager.eventM.CreateANot("Bought!");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && phase > 0)
        {
            phase--;
            UpdatePhaseSelection();
        }
    }

    public void NextPhase()
    {
        phase++;
        UpdatePhaseSelection();
    }

    public void UpdatePhaseSelection()
    {
        if(phase == 0)
        {
            difficultId = -1;
            biomeArea.SetActive(true);
            difficultiesArea.SetActive(false);
            buttonArea.SetActive(false);
            if(biomeId != -1 && !randomBiomeMode) { UpdateNextPhaseButton(true); }
            else if(biomeId == -1 && randomBiomeMode) { UpdateNextPhaseButton(true); }
            else { UpdateNextPhaseButton(false); }
        }
        else if(phase == 1)
        {
            biomeArea.SetActive(false);
            difficultiesArea.SetActive(true);
            buttonArea.SetActive(false);
            UpdateDifficultsPanel();
            if(difficultId != -1 && !randomDifficultMode) { UpdateNextPhaseButton(true); }
            else if(biomeId == -1){ SetRandomDifficulties(true); randomDifficultsToggle.gameObject.SetActive(false); }
            else { UpdateNextPhaseButton(false); }
        }
        else if(phase == 2)
        {
            biomeArea.SetActive(false);
            difficultiesArea.SetActive(false);

            nextPhase.SetActive(false);

            buttonArea.SetActive(true);
            UpdateButtonArea();
        }
    }

    public void UpdateNextPhaseButton(bool myIdIsSetted)
    {
        if (myIdIsSetted)
        {
            nextPhase.SetActive(true);
        }
        else if (!myIdIsSetted)
        {
            nextPhase.SetActive(false);
        }
    }
}
