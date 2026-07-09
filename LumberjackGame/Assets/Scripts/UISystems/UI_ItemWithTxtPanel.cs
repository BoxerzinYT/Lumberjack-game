using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_ItemWithTxtPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myTxt;
    [SerializeField] Image myImage;
    [SerializeField] Button myButton;
    public Button MyButton { get { return myButton; }}
    [SerializeField] GameObject selectedBg;
    public GameObject SelectedBg { get { return selectedBg; }}
    [SerializeField] GameObject lockerBg;
    public GameObject LockerBg { get { return lockerBg; }}
    [SerializeField] Button lockerButton;
    public Button LockerButton { get { return lockerButton; }}

    public void Set(string _txt, Sprite _image, bool hasButton, UnityEvent buttonEvent)
    {
        myTxt.text = _txt;
        myImage.sprite = _image;

        if (hasButton)
        {
            myButton.onClick = new Button.ButtonClickedEvent();
            myButton.onClick.AddListener(() => buttonEvent.Invoke());
        }
    }
}
