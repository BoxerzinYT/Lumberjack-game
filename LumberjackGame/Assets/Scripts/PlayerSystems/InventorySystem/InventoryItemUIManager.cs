using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItemUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //ToolTipPopUp toolTipPopUp;
    ShowInventoryItens sii;
    HectorInventory hj;
    [SerializeField] TextMeshProUGUI stackTxt;
    [SerializeField] Button myButton;
    [SerializeField] Image itemImage;
    [SerializeField] Image bigImage;
    [SerializeField] Image rarityBg;
    [SerializeField] Image biomeIcon;
    [SerializeField] Image rankIcon;
    [SerializeField] Animator rankIconAnim;

    [Header("Sell")]
    bool selling;
    public bool Selling { get { return selling; } set { selling = value; } }
    /*
    [Header("IWL")]
    [SerializeField] GameObject iwl_levelBarObj;
    [SerializeField] Image iwl_levelBar;
    [Header("FavoriteSettings")]
    [SerializeField] GameObject settingsTranspa;
    [SerializeField] Button favoriteButton;
    [SerializeField] Image favoriteIcon;
    [SerializeField] Sprite[] favoriteButtonSprites;
    [Header("LockedSettings")]
    [SerializeField] Button lockedButton;
    [SerializeField] Image lockedIcon;
    [SerializeField] Sprite[] lockedButtonSprites;

    
    [Header("DragAndDropSystem")]
    [SerializeField] bool canDrag;
    [SerializeField] RectTransform rectT;
    Transform parentBeforeDrag;
    Transform parentAfterDrag;
    [SerializeField] CanvasGroup canvasGroup;
    Canvas canvas;

    
    UnityEvent whenDropInSomePlace;
    UnityEvent whenStartDrag;
    UnityEvent whenEndDrag;
    public UnityEvent WhenDropInSomePlace { get { return whenDropInSomePlace; } set { whenDropInSomePlace = value; } }
    public UnityEvent WhenStartDrag { get { return whenStartDrag; } set { whenStartDrag = value; } }
    public UnityEvent WhenEndDrag { get { return whenEndDrag; } set { whenEndDrag = value; } }
    public Transform ParentAfterDrag { get { return parentAfterDrag; } set { parentAfterDrag = value; } }
    public Transform ParentBeforeDrag { get { return parentBeforeDrag; } set { parentBeforeDrag = value; } }
    public bool CanDrag { get { return canDrag; } set { canDrag = value; } }
    */

    [Header("BreakItemSystem")]
    //[SerializeField] TextMeshProUGUI breakTxt;

    Item myItem;
    InventoryItem myInventItem;

    public ShowInventoryItens Sii { set { sii = value; } }
    public TextMeshProUGUI StackTxt { get { return stackTxt; } }
    public Button MyButton { get { return myButton; } }
    public Image ItemImage { get { return itemImage; } }
    public Image BigImage { get { return bigImage; } }

    public Item MyItem { get { return myItem; } set { myItem = value; } }
    public InventoryItem MyInventItem { get { return myInventItem; } }

    bool isAShowItem;
    public bool IsAShowItem { get { return isAShowItem; } set { isAShowItem = value; } }

    private void Awake()
    {
        //toolTipPopUp = GameObject.FindObjectOfType<ToolTipPopUp>();
        //canvas = GameObject.FindFirstObjectByType<HUDLoad>().GetComponent<Canvas>();
    }

    public void FinishSettings(bool isInventItem, InventoryItem InventItem, Item item, float quant)
    {
        if (isInventItem == true)
        {
            myInventItem = InventItem;
            stackTxt.text = EventsManager.eventM.UpdateVariables(InventItem.stackSize);
            if(!InventItem.itemData.canChangeMyRank) { rankIcon.gameObject.SetActive(false); }
            rarityBg.color = InventItem.itemData.itemRarity.rarityColor;
            rankIcon.sprite = InventItem.itemRank.rankIcon;
            if(InventItem.itemRank.rankId == 7) { rankIconAnim.SetTrigger("Rainbow"); }
            //biomeIcon.sprite = InventItem.itemData.itemBiome.biomeIcon;
            myItem = InventItem.itemData;

            if(InventItem.itemData.isStackable)
            {
                BigImage.gameObject.SetActive(false);
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = myItem.itemIcon;
            }
            else
            {
                itemImage.gameObject.SetActive(false);
                bigImage.gameObject.SetActive(true);
                BigImage.sprite = myItem.itemIcon;
                stackTxt.gameObject.SetActive(false);
            }
            /*
            if (InventItem.canBreak == true)
            {
                breakTxt.gameObject.SetActive(true);
                breakTxt.text = EventsManager.eventM.UpdateVariables(InventItem.quantAlreadyBreaked) + "/" + EventsManager.eventM.UpdateVariables(InventItem.quantToBreak);
            }
            
            else { breakTxt.gameObject.SetActive(false); }
            */

            /*
            if (IsAShowItem == false)
            {
                if (myItem.GetType() == typeof(Tools))
                {
                    iwl_levelBarObj.gameObject.SetActive(true);
                    Tools t = (Tools)myItem;
                    iwl_levelBar.fillAmount = InventItem.xpGained / t.eachLevelUp[myInventItem.level].xpForNextLevel;
                }

                if (FindObjectOfType<UsableItemList>() == true && myItem.GetType() == typeof(PUItens))
                {
                    hj = FindObjectOfType<UsableItemList>();

                    MyButton.onClick = new Button.ButtonClickedEvent();
                    MyButton.onClick.AddListener(() => hj.AddToList((PUItens)myItem));
                    MyButton.onClick.AddListener(() => RemoveItem(myInventItem, 1));
                }
            }

            if (isInventItem == true)
            {
                SettingsModeDesactivated();
            }
            */
        }
        else
        {
            myItem = item;
            stackTxt.text = quant.ToString("F0");
        }
    }

    public void RemoveItem(InventoryItem item, int amount)
    {
        hj.GetComponent<HectorInventory>().hectorInventory.RemoveItem(1, myInventItem);
        sii.ShowItensInUI(hj.GetComponent<HectorInventory>().hectorInventory.inventory);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //toolTipPopUp.DisplayInfo(myInventItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //toolTipPopUp.HideInfo();
    }
    

    /*
    public void ChangeFavoriteSettings()
    {
        if(myInventItem.favorite == false)
        {
            myInventItem.ChangeFavorite(true);
            favoriteButton.GetComponent<Image>().sprite = favoriteButtonSprites[1];
        }
        else if (myInventItem.favorite == true)
        {
            myInventItem.ChangeFavorite(false);
            favoriteButton.GetComponent<Image>().sprite = favoriteButtonSprites[0];
        }
    }
    public void ChangeLockedSettings()
    {
        if (myInventItem.locked == false)
        {
            myInventItem.ChangeLocked(true);
            lockedButton.GetComponent<Image>().sprite = lockedButtonSprites[1];
        }
        else if (myInventItem.locked == true)
        {
            myInventItem.ChangeLocked(false);
            lockedButton.GetComponent<Image>().sprite = lockedButtonSprites[0];
        }
    }

    public void SettingsModeActivated()
    {
        settingsTranspa.SetActive(true);

        favoriteIcon.gameObject.SetActive(false);
        lockedIcon.gameObject.SetActive(false);

        favoriteButton.gameObject.SetActive(true);
        lockedButton.gameObject.SetActive(true);

        if (myInventItem.favorite == true) { favoriteButton.GetComponent<Image>().sprite = favoriteButtonSprites[1]; }
        if (myInventItem.locked == true) { lockedButton.GetComponent<Image>().sprite = lockedButtonSprites[1]; }
    }
    public void SettingsModeDesactivated()
    {
        settingsTranspa.SetActive(false);

        favoriteButton.gameObject.SetActive(false);
        lockedButton.gameObject.SetActive(false);

        if (myInventItem.favorite == true) { favoriteIcon.gameObject.SetActive(true); favoriteIcon.sprite = favoriteButtonSprites[1]; }
        else { favoriteIcon.gameObject.SetActive(false); }

        if (myInventItem.locked == true) { lockedIcon.gameObject.SetActive(true); lockedIcon.sprite = lockedButtonSprites[1]; }
        else { lockedIcon.gameObject.SetActive(false); }
    }
    */

    /*
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(canDrag == true)
        {
            DragManager[] dragM = GameObject.FindObjectsOfType<DragManager>();
            foreach(var drag in dragM) { drag.EnableDropFunction(); }
            parentBeforeDrag = transform.parent;
            parentAfterDrag = transform.parent;
            transform.SetParent(canvas.transform);
            transform.SetAsLastSibling();
            canvasGroup.blocksRaycasts = false;

            if(whenStartDrag != null) { whenStartDrag.Invoke(); }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(canDrag == true)
        {
            rectT.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(canDrag == true)
        {
            DragManager[] dragM = GameObject.FindObjectsOfType<DragManager>();
            foreach (var drag in dragM) { drag.DisableDropFunction(); }
            transform.SetParent(parentAfterDrag);
            canvasGroup.blocksRaycasts = true;

            if (whenEndDrag != null) { whenEndDrag.Invoke(); }
        }
    }

    public void OnEndDragManual(Transform parentAfterDrag_)
    {
        DragManager[] dragM = GameObject.FindObjectsOfType<DragManager>();
        foreach (var drag in dragM) { drag.DisableDropFunction(); }
        transform.SetParent(parentAfterDrag_);
        canvasGroup.blocksRaycasts = true;
    }
    */
}
