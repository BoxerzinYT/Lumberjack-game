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
        if(whenOpen != null)
        {
            whenOpen.Invoke();
        }
    }

    public void NormalClose()
    {
        panel.SetActive(false);
        if(whenClose != null)
        {
            whenClose.Invoke();
        }
    }

    public void DestroyClose()
    {
        Destroy(this.gameObject);
        if(whenCloseDestroy != null)
        {
            whenCloseDestroy.Invoke();
        }
    }
}
