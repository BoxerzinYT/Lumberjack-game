using TMPro;
using UnityEngine;

public class UI_ChangeAText : MonoBehaviour
{
    public void ChangeAText(TextMeshProUGUI text, string stringTxt)
    {
        text.text = stringTxt;
    }
}
