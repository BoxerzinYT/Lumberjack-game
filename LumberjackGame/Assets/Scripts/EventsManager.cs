using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class EventsManager : MonoBehaviour
{
    public static EventsManager eventM { get; private set; }

    public bool playerCanWalk;
    public bool playerCanInteract;

    [Header("LoadingScreen")]
    [SerializeField] GameObject loadingScreen;

    void Awake()
    {
        if (eventM == null)
        {
            eventM = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (eventM != null)
        {
            Destroy(gameObject);
        }
    }

    public string UpdateVariables(float var)
    {
        string varTxt = var.ToString();

        if (var >= 1000000) { varTxt = (var / 1000000).ToString("F2") + "M"; }
        else if (var >= 1000) { varTxt = (var / 1000).ToString("F2") + "K"; }
        else if (var < 1000)
        {
            if (varTxt.Contains(","))
            {
                varTxt = var.ToString("F2");
            }
            else
            {
                varTxt = var.ToString("F0");
            }
        }

        return varTxt;
    }

    public void UpdateBar(float min, float max, Image _bar, Gradient _gradient)
    {
        float amountCal = min / max;
        _bar.fillAmount = amountCal;
        _bar.color = _gradient.Evaluate(amountCal);
    }

    public void ChangeAText(TextMeshProUGUI text, string stringTxt)
    {
        text.text = stringTxt;
    }

    public void OpenAUI()
    {
        playerCanInteract = false;
        playerCanWalk = false;
    }

    public void CloseAUI()
    {
        playerCanInteract = true;
        playerCanWalk = true;
    }

    public void GoToAnotherScene(List<String> scenesToLoad, List<String> scenesToUnload)
    {
        GameObject newLoadingScreen = Instantiate(loadingScreen);
        StartCoroutine(newLoadingScreen.GetComponent<LoadingScreenManager>().LoadingIE(scenesToLoad, scenesToUnload));
        playerCanInteract = false;
        playerCanWalk = false;
    }
}
