using UnityEngine;
using UnityEngine.Events;

public class UI_PanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] UnityEvent whenOpen;
    [SerializeField] UnityEvent whenClose;
    [SerializeField] UnityEvent whenCloseDestroy;

    public void Open()
    {
        panel.SetActive(true);
        EventsManager.eventM.OpenAUI();
        if(whenOpen != null)
        {
            whenOpen.Invoke();
        }
    }

    public void NormalClose()
    {
        panel.SetActive(false);
        EventsManager.eventM.CloseAUI();
        if(whenClose != null)
        {
            whenClose.Invoke();
        }
    }

    public void DestroyClose()
    {
        Destroy(this.gameObject);
        EventsManager.eventM.CloseAUI();
        if(whenCloseDestroy != null)
        {
            whenCloseDestroy.Invoke();
        }
    }
}
