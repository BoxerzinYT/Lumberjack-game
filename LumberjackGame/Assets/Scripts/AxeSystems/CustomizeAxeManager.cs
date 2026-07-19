using UnityEngine;

public class CustomizeAxeManager : MonoBehaviour
{
    [SerializeField] Sprite foiceSprite_body;
    [SerializeField] Sprite normalSprite_body;
    [SerializeField] Sprite foiceSprite_head;
    [SerializeField] Sprite normalSprite_head;
    [SerializeField] SpriteRenderer axeHeadSp;
    public SpriteRenderer AxeHeadSp { get { return axeHeadSp; }}
    [SerializeField] SpriteRenderer axeBodySp;
    public SpriteRenderer AxeBodySp { get { return axeBodySp; }}
    bool equipped;
    bool equipped2;

    public void SetBody(InventoryItem inventItem)
    {
        AxePart newBody = (AxePart)inventItem.itemData;
        axeBodySp.sprite = newBody.partSprite;
        Vector2 axeHeadPos = axeHeadSp.transform.localPosition;
        axeHeadPos.x = newBody.partSprite.rect.width / newBody.partSprite.pixelsPerUnit * 0.25f;
        axeHeadPos.y = newBody.partSprite.rect.height / newBody.partSprite.pixelsPerUnit * 0.75f;
        axeHeadSp.transform.localPosition = axeHeadPos;
    }
    public void SetHead(InventoryItem inventItem)
    {
        AxePart newHead = (AxePart)inventItem.itemData;
        axeHeadSp.sprite = newHead.partSprite;
    }

    public void CalculateHeigh()
    {
        if (!equipped)
        {
            //Debug.Log(foiceSprite.rect.height / foiceSprite.pixelsPerUnit);
            axeBodySp.sprite = foiceSprite_body;
            Vector2 axeHeadPos = axeHeadSp.transform.localPosition;
            axeHeadPos.x = foiceSprite_body.rect.width / foiceSprite_body.pixelsPerUnit * 0.25f;
            axeHeadPos.y = foiceSprite_body.rect.height / foiceSprite_body.pixelsPerUnit * 0.75f;
            axeHeadSp.transform.localPosition = axeHeadPos;
            equipped = true;
        }
        else
        {
            axeBodySp.sprite = normalSprite_body;
            Vector2 axeHeadPos = axeHeadSp.transform.localPosition;
            axeHeadPos.x = normalSprite_body.rect.width / normalSprite_body.pixelsPerUnit * 0.25f;
            axeHeadPos.y = normalSprite_body.rect.height / normalSprite_body.pixelsPerUnit * 0.75f;
            axeHeadSp.transform.localPosition = axeHeadPos;
            equipped = false;
        }
    }

    public void PutHead()
    {
        if (!equipped2)
        {
            axeHeadSp.sprite = foiceSprite_head;
            equipped2 = true;
        }
        else
        {
            axeHeadSp.sprite = normalSprite_head;
            equipped2 = false;
        }
    }
}
