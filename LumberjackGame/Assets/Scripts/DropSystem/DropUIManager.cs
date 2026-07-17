using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropUIManager : MonoBehaviour
{
    [SerializeField] RectTransform myRect;
    [SerializeField] TextMeshProUGUI dropQuantTxt;
    [SerializeField] Image dropImage;
    [SerializeField] float xUp;
    [SerializeField] float yUp;
    [SerializeField] float speed;
    Vector2 alwaysAddInRect;
    bool canRun;

    public void Start()
    {
        alwaysAddInRect = new Vector2(myRect.anchoredPosition.x + Random.Range(-xUp, xUp) * 2.5f, myRect.anchoredPosition.y + Random.Range(yUp / 2, yUp) * 2.5f);
        canRun = true;
        Destroy(this.gameObject, 3f);
    }

    public void SetDrop(Sprite _dropImage, string quant)
    {
        dropImage.sprite = _dropImage;
        dropQuantTxt.text = "x" + quant;
    }

    public void Update()
    {
        if(!canRun) return;
        myRect.anchoredPosition = Vector2.Lerp(myRect.anchoredPosition, alwaysAddInRect, speed * Time.deltaTime);
    }
}
