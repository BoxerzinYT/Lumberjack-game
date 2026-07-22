using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Struc_CharacterUpgrade : StructureSystem
{
    [Header("GeneralSettings")]
    [SerializeField] Image hectorInSelection_image;
    [SerializeField] Button hectorInSelection_button;
    [SerializeField] GameObject characterInfoPanel;
    [SerializeField] TextMeshProUGUI characterNameTxt;
    [SerializeField] TextMeshProUGUI characterDescriptionTxt;

    [Header("MultsInfoSettings")]
    [SerializeField] GameObject globalMultPanel;
    [SerializeField] GameObject localMultPanel;
    [SerializeField] MultInfoPanel[] globalMultInfoPanels;
    [SerializeField] MultInfoPanel[] hec_passiveMultInfo;
    [SerializeField] TextMeshProUGUI coinsTxt;
    [SerializeField] Button upgradeButton;
    int currentPanelSelectedId;

    public void WhenOpenPanel()
    {
        currentPanelSelectedId = 0;
        globalMultPanel.SetActive(true);
        localMultPanel.SetActive(false);

        SetSelectionPanel();
        characterInfoPanel.SetActive(false);
    }

    public void SetSelectionPanel()
    {
        //Quando adicionar mais personagens, mudar esse sistem para um foreach que configura cada
        //botão para abrir um character diferente baseado no hecStats

        hectorInSelection_button.onClick = new Button.ButtonClickedEvent();
        hectorInSelection_button.onClick.AddListener(() => LoadInfoAboutCharacter(LastPlayerThatPassHere.characterSelectedStatus));
    }
    public void LoadInfoAboutCharacter(CharactersBaseStats characterSelected)
    {
        characterInfoPanel.SetActive(true);

        characterNameTxt.text = characterSelected.characterName;
        characterNameTxt.text = characterSelected.characterDescription;

        //Global chances first
        for(int i=0; i<globalMultInfoPanels.Length; i++)
        {
            if(i != 3 && i != 4)
            {
                globalMultInfoPanels[i].oldValueTxt.text = LastPlayerThatPassHere.GetACharacterMultValue(i, 0).ToString("F2");
                globalMultInfoPanels[i].newValueTxt.text = (LastPlayerThatPassHere.GetACharacterMultValue(i, 0)
                + LastPlayerThatPassHere.charactersData[0].incrementsPerLevel[i] * (LastPlayerThatPassHere.LevelMan.hector_level + 1)).ToString("f2");
            }
            else
            {
                globalMultInfoPanels[i].oldValueTxt.text = (LastPlayerThatPassHere.GetACharacterMultValue(i, 0) * 100).ToString("F2");
                globalMultInfoPanels[i].newValueTxt.text = ((LastPlayerThatPassHere.GetACharacterMultValue(i, 0)
                + LastPlayerThatPassHere.charactersData[0].incrementsPerLevel[i] * (LastPlayerThatPassHere.LevelMan.hector_level + 1)) * 100).ToString("f2");
            }
            globalMultInfoPanels[i].newValueTxt.color = Color.green;
            if(i == 2 || i == 5 || i == 6)
            {
                globalMultInfoPanels[i].oldValueTxt.text += "x";
                globalMultInfoPanels[i].newValueTxt.text += "x";
            }
            else if(i == 3 || i == 4)
            {
                globalMultInfoPanels[i].oldValueTxt.text += "%";
                globalMultInfoPanels[i].newValueTxt.text += "%";
            }
        }
        //next, the passive upgrades
        IsTheHector();
        float priceMult = 1 + LastPlayerThatPassHere.charactersData[0].priceMult * LastPlayerThatPassHere.LevelMan.hector_level;
        float price = LastPlayerThatPassHere.charactersData[0].firstPrice * priceMult;
        coinsTxt.text = EventsManager.eventM.UpdateVariables(price);
        upgradeButton.onClick = new Button.ButtonClickedEvent();
        upgradeButton.onClick.AddListener(() => Upgrade(price));
    }

    public void Upgrade(float price)
    {
        if(HasCoins(LastPlayerThatPassHere.Cha_coins, price))
        {
            LastPlayerThatPassHere.LevelMan.hector_level += 1;
            LastPlayerThatPassHere.LoadSelectedCharacterStatus();
            LoadInfoAboutCharacter(LastPlayerThatPassHere.characterSelectedStatus);
            BuyWithCoins(LastPlayerThatPassHere, price);
        }
    }

    public void IsTheHector()
    {
        Char_Hector charHector = (Char_Hector)LastPlayerThatPassHere.charactersData[0];

        for(int i=0; i<hec_passiveMultInfo.Length; i++)
        {
            if(i != 3 && i != 4)
            {
                hec_passiveMultInfo[i].oldValueTxt.text = 
                (charHector.GetUpgradeValueById(i) + charHector.passiveIncrementsPerLevel[i] * LastPlayerThatPassHere.LevelMan.hector_level).ToString("F2");
                hec_passiveMultInfo[i].newValueTxt.text = 
                (charHector.GetUpgradeValueById(i) + charHector.passiveIncrementsPerLevel[i] * (LastPlayerThatPassHere.LevelMan.hector_level + 1)).ToString("F2");
            }
            else
            {
                hec_passiveMultInfo[i].oldValueTxt.text = 
                ((charHector.GetUpgradeValueById(i) + charHector.passiveIncrementsPerLevel[i] * LastPlayerThatPassHere.LevelMan.hector_level) * 100).ToString("F2");
                hec_passiveMultInfo[i].newValueTxt.text = 
                ((charHector.GetUpgradeValueById(i) + charHector.passiveIncrementsPerLevel[i] * (LastPlayerThatPassHere.LevelMan.hector_level + 1)) * 100).ToString("F2");
            }
            hec_passiveMultInfo[i].newValueTxt.color = Color.green;
            if(i == 4 || i == 6)
            {
                hec_passiveMultInfo[i].oldValueTxt.text += "x";
                hec_passiveMultInfo[i].newValueTxt.text += "x";
            }
            else if(i == 1 || i == 3 || i == 5 || i == 7)
            {
                hec_passiveMultInfo[i].oldValueTxt.text += "s";
                hec_passiveMultInfo[i].newValueTxt.text += "s";
            }
        }
    }

    public void OpenUpgradePanelByButtons(int id)
    {
        currentPanelSelectedId = id;
        if(currentPanelSelectedId == 0)
        {
            globalMultPanel.SetActive(false);
            localMultPanel.SetActive(true);
        }
        else if(currentPanelSelectedId == 1)
        {
            globalMultPanel.SetActive(true);
            localMultPanel.SetActive(false);
        }
    }

}
