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
    [SerializeField] List<string> scenesToLoad;
    [SerializeField] List<string> scenesToUnload;

    [Header("NotificationSettings")]
    [SerializeField] UI_Notification notiPrefab;
    Transform notiContent;

    List<Global_TimerSystem> timers = new List<Global_TimerSystem>();

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

    public void Start()
    {
        notiContent = FindFirstObjectByType<UI_NotificationCanvas>().NotiContent;
    }

    public void Update()
    {
        for(int i=0; i< timers.Count; i++)
        {
            timers[i].Update(Time.deltaTime);
            if (timers[i].TimerFinised)
            {
                timers.Remove(timers[i]);
            }
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

    public bool CalculateAChance(float chance, float itemChance, Hec_Stats hec_Stats)
    {
        if(chance <= itemChance * hec_Stats.GetACharacterMultValue(6, hec_Stats.characterSelectedId) * hec_Stats.All_luckMult)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateBar(float min, float max, Image _bar, Gradient _gradient)
    {
        float amountCal = min / max;
        _bar.fillAmount = amountCal;
        if(_gradient == null) { return; }
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

    public void GoToAnotherScene()
    {
        if(scenesToLoad.Count <= 0 || scenesToUnload.Count <= 0) { EventsManager.eventM.CreateANot("Not to load!"); return; }
        GameObject newLoadingScreen = Instantiate(loadingScreen);
        StartCoroutine(newLoadingScreen.GetComponent<LoadingScreenManager>().LoadingIE(scenesToLoad, scenesToUnload));
        playerCanInteract = false;
        playerCanWalk = false;
    }

    public void AddScenes(string sceneName, int listId) 
    {
        //Listid == 0 significa colocar uma cena para carregar
        //Listid == 1 significa colocar uma cena para descarregar, excluir
        if(listId == 0)
        {
            scenesToLoad.Add(sceneName);
        }
        else
        {
            scenesToUnload.Add(sceneName);
        }
    }

    public void ClearScenesList()
    {
        scenesToLoad.Clear();
        scenesToUnload.Clear();
    }

    public void CreateANot(string txt)
    {
        UI_Notification newNoti = Instantiate(notiPrefab, notiContent);
        newNoti.SetNoti(txt);
        newNoti.transform.SetAsFirstSibling();
        //Debug.Log("Created new noti!");
    }

    public Global_TimerSystem CreateTimer(float duration, Action whenComplete, bool canStartTimer)
    {
        Global_TimerSystem newTimer = new Global_TimerSystem();
        newTimer.SetTimer(duration, whenComplete, canStartTimer);

        timers.Add(newTimer);
        return newTimer;
    }
}
