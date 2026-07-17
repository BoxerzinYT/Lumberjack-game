using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Expedition_ResultsCalculation : Qol_BuySystem
{
    [SerializeField] float allStarValue;
    [SerializeField] float halfStarValue;

    GlobalExpeditionManager myExpeditionManager;
    [Header("UI")]
    [SerializeField] InventoryItem[] biomeCoins;
    [SerializeField] GameObject resultsScreenPanel;
    [SerializeField] Image starsFill;
    [SerializeField] TextMeshProUGUI objectivesTxt;
    [SerializeField] UI_ItemWithTxtPanel biomeCoinPanel;
    [SerializeField] Button continueButton;
    [SerializeField] Button goBackButton;

    public void Start()
    {
        myExpeditionManager = GetComponent<GlobalExpeditionManager>();
    }

    public IEnumerator ShowResults(int modeIdSelected, int biomeId, bool isWon)
    {
        resultsScreenPanel.SetActive(true);
        starsFill.fillAmount = 0;
        starsFill.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        objectivesTxt.gameObject.SetActive(true);

        //First, the objective
        if (myExpeditionManager.completedFirst && myExpeditionManager.completedSecond)
        {
            starsFill.fillAmount += allStarValue;
            objectivesTxt.text = "Completed the normal + over objective! <color=yellow>+1 star</color>";
        }
        else if(myExpeditionManager.completedFirst && !myExpeditionManager.completedSecond)
        {
            starsFill.fillAmount += halfStarValue;
            objectivesTxt.text = "Completed the normal objective! <color=yellow>+0,5 star</color>";
        }
        else
        {
            objectivesTxt.text = "You lost.";
        }

        yield return new WaitForSeconds(1.5f);

        if (myExpeditionManager.completedFirst)
        {
            objectivesTxt.text = "Stunned: " + myExpeditionManager.stunnedTimes + "/" + myExpeditionManager.allStunsSituations;
            if(myExpeditionManager.stunnedTimes == 0)
            {
                starsFill.fillAmount += allStarValue;
                objectivesTxt.text += ". <color=yellow> +1 Star! </color>";
            }
            else if(myExpeditionManager.stunnedTimes < myExpeditionManager.allStunsSituations / 2)
            {
                starsFill.fillAmount += halfStarValue;
                objectivesTxt.text += ". <color=yellow> +0,5 Star </color>";
            }
            else
            {
                objectivesTxt.text += ". <color=yellow> +0 Star </color>";
            }
        }

        yield return new WaitForSeconds(1.5f);

        objectivesTxt.gameObject.SetActive(false);

        int biomeCoinsCollected = Mathf.RoundToInt(100 * starsFill.fillAmount);

        biomeCoinPanel.gameObject.SetActive(true);
        biomeCoinPanel.Set(biomeCoinsCollected.ToString("F0"), biomeCoins[biomeId].itemData.itemIcon, false, null);

        CollectItem(myExpeditionManager.getExpeditionInfo.PlayerInfos, biomeCoins[biomeId], biomeCoinsCollected);

        yield return new WaitForSeconds(1.5f);

        if (isWon)
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            goBackButton.gameObject.SetActive(true);
        }

        StopCoroutine("ShowResults");
    }

    public void Continue()
    {
        myExpeditionManager.getExpeditionInfo.PlayerInfos.GetComponent<Animator>().SetBool("Won", false);

        EventsManager.eventM.playerCanInteract = true;
        EventsManager.eventM.playerCanWalk = true;
        resultsScreenPanel.SetActive(false);
    }
}
