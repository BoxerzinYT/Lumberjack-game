using TMPro;
using UnityEngine;

public class UI_TextInScreen : MonoBehaviour
{
    [SerializeField] RectTransform myRec;
    [SerializeField] Animator myAnim;
    public Animator MyAnim { get { return myAnim; }}
    [SerializeField] TextMeshProUGUI myTxt;
    [SerializeField] float maxRotation;
    [SerializeField] float maxX;

    public void Start()
    {
        float x = Random.Range(-maxX, maxX);
        float magnitude = x / maxX;
        float rotation = maxRotation * magnitude;
        myRec.anchoredPosition = new Vector2(x, 0);
        myRec.eulerAngles = new Vector3(0,0, rotation);
        Destroy(this.gameObject, 2f);
    }

    public void SetTxt(string txt, Color txtColor)
    {
        myTxt.text = txt;
        myTxt.color = txtColor;
    }
}