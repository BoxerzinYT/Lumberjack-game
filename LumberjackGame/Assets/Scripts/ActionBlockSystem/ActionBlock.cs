using UnityEngine;
using UnityEngine.Events;

public class ActionBlock : Qol_BuySystem
{
    [SerializeField] GameObject parts;
    bool activated;
    Hec_Stats player;
    [SerializeField] UnityEvent whenPlayerClick;
    public Hec_Stats Player { get { return player; } }

    public void Start()
    {
        DesactivateBlock();
    }

    public void ActivateActionBlock(Hec_Stats _player)
    {
        parts.SetActive(true);
        activated = true;
        player = _player;
    }
    public void DesactivateBlock()
    {
        parts.SetActive(false);
        activated = false;
        player = null;
    }
    public void OnMouseDown()
    {
        if (activated)
        {
            if(whenPlayerClick != null)
            {
                whenPlayerClick.Invoke();
            }
        }
    }
}
