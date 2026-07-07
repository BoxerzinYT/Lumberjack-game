using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Hec_ActionSystem : MonoBehaviour
{
    [SerializeField] Acb_InventorySettings acb_inventSettings;
    Hec_Stats myStats;
    bool isInActionMode;
    ActionBlock[] blocks;

    ActionItem actionItemSelected;
    InventoryItem actionInventItem;
    public ActionItem ActionItemSelected { get { return actionItemSelected; } }
    public InventoryItem ActionInventItem { get { return actionInventItem; } }

    [SerializeField] UnityEvent whenStartAction;
    [SerializeField] UnityEvent whenEndAction;

    [Header("UISettings")]
    [SerializeField] Image actionItemImage;
    [SerializeField] TextMeshProUGUI actionItemQuantTxt;

    public void Start()
    {
        myStats = GetComponent<Hec_Stats>();
    }

    public void StartAction(ActionItem _actionItemCurrentlyActivated, InventoryItem _actionInventItem)
    {
        actionItemSelected = _actionItemCurrentlyActivated;
        actionInventItem = _actionInventItem;
        if(whenStartAction != null) { whenStartAction.Invoke(); }
        ShowActions(actionItemSelected.typeOfAction);

        actionItemImage.sprite = actionItemSelected.itemIcon;
        UpdateActionItemInHUD();
    }

    public void UpdateActionItemInHUD()
    {
        actionItemQuantTxt.text = "x" + EventsManager.eventM.UpdateVariables(actionInventItem.stackSize);
    }

    public void ShowActions(TypeOfAction _typeOfAction)
    {
        if (!isInActionMode)
        {
            switch (_typeOfAction)
            {
                case TypeOfAction.sappling:
                blocks = FindObjectsByType<Acb_SaplingSlot>(0);
                break;
                case TypeOfAction.expansion:
                blocks = FindObjectsByType<Acb_ExpandMap>(0);
                break;
            }
            foreach(var a in blocks)
            {
                a.ActivateActionBlock(myStats);
            }
            isInActionMode = true;
        }
    }

    public void Update()
    {
        if(isInActionMode && Input.GetKeyDown(KeyCode.Q))
        {
            CloseAction();
            isInActionMode = false;
        }
    }

    public void CloseAction()
    {
        foreach(var a in blocks)
        {
            if(a!= null)
            {
                a.DesactivateBlock();
            }
        }
        blocks = null;
        actionItemSelected = null;

        if(whenEndAction != null) { whenEndAction.Invoke(); }

        isInActionMode = false;
    }
}
