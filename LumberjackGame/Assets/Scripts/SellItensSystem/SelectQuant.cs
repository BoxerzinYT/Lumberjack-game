using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(UI_PanelManager))]
public class SelectQuant : MonoBehaviour
{
    [SerializeField] TMP_InputField inputF;
    [SerializeField] Slider slider;
    public Slider Slider { get { return slider; } set { slider = value; } }
    [SerializeField] Button selectButton;
    public Button SelectButton { get { return selectButton; } set { selectButton = value; } }

    public void ChangeInputFieldValue(string value)
    {
        slider.value = int.Parse(value);
        if (int.Parse(value) > slider.maxValue) { slider.value = (int)slider.maxValue; }
        UpdateTotalText();
    }

    public int GetValue()
    {
        return (int)slider.value;
    }

    public void Max()
    {
        slider.value = (int)slider.maxValue;
    }

    public void UpdateTotalText()
    {
        inputF.text = slider.value.ToString("F0");
    }
}
