using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_UpdateBars : MonoBehaviour
{
    public void UpdateBar(float min, float max, Image _bar, Gradient _gradient, bool veTxt, TextMeshProUGUI _txt)
    {
        float amountCal = min / max;
        _bar.fillAmount = amountCal;
        _bar.color = _gradient.Evaluate(amountCal);
        if (veTxt)
        {
            _txt.text = min.ToString("F0") + "/" + max.ToString("F0");
        }
    }
}
