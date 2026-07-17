using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    Animator anim;
    Slider slider;
    GameObject panelSave;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        anim = transform.Find("LoadingScreen").GetComponent<Animator>();
        slider = transform.Find("LoadingScreen").transform.Find("Slider").GetComponent<Slider>();
    }

    public IEnumerator LoadingIE(List<String> scenesToLoad, List<String> scenesToUnload)
    {
        EventsManager.eventM.playerCanWalk = false;
        EventsManager.eventM.playerCanInteract = false;

        yield return new WaitForSeconds(1f);
        //anim.SetTrigger("Idle");

        foreach (var s in scenesToUnload)
        {
            AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(s);
        }

        yield return new WaitForSeconds(1f);
        
        foreach (var s in scenesToLoad)
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
        }

        anim.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

        EventsManager.eventM.playerCanWalk = true;
        EventsManager.eventM.playerCanInteract = true;
        EventsManager.eventM.ClearScenesList();
        
        StopCoroutine(LoadingIE(null, null));
    }
}
