using TMPro;
using UnityEngine;

public class UI_Notification : MonoBehaviour
{
    [SerializeField] Animator notiAnim;
    [SerializeField] TextMeshProUGUI notiTxt;

    public void SetNoti(string txt)
    {
        notiTxt.text = txt;
        notiAnim.SetTrigger("NormalNoti");
        Destroy(gameObject, 2.5f);
        /*
        switch (type)
        {
            case typeOfNot.normal:
            notiAnim.SetTrigger("NormalNoti");
            notiTxt.color = Color.white;
            break;
            case typeOfNot.error:
            notiAnim.SetTrigger("ErrorNoti");
            notiTxt.color = Color.red;
            break;
        }
        */
    }


}
[System.Serializable]
public enum typeOfNot
{
    normal,
    error,
    teleport //temporary unabled
};
