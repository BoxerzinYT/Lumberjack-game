using System;
using UnityEngine;

public class Passive_HitTarget : MonoBehaviour
{
    public static event Action onHit;
    [SerializeField] GameObject passiveBarObj;
    [SerializeField] RectTransform bar;
    [SerializeField] RectTransform target;
    [SerializeField] RectTransform point;

    [SerializeField] float speed;
    bool activated;

    float upLimit;
    float downLimit;
    int direction;

    bool pressed;
    public bool Pressed { get { return pressed; }}

    public void Start()
    {
        float halfHeight = bar.rect.height / 2f;

        downLimit = -halfHeight;
        upLimit = halfHeight;

        passiveBarObj.SetActive(false);
    }

    public void Activate()
    {
        passiveBarObj.gameObject.SetActive(true);
        point.anchoredPosition = new Vector2(0, upLimit);
        target.anchoredPosition = new Vector2(0, UnityEngine.Random.Range(downLimit, upLimit));
        activated = true;
        pressed = true;
    }

    public void Desactivate()
    {
        passiveBarObj.gameObject.SetActive(false);
        pressed = false;
        activated = false;
    }

    public void Update()
    {
        if(!activated) { return; }
        MovePoint();
    }

    public void UsedAxe()
    {
        activated = false;
        //pressed = true;
        WhenPressed();
    }

    public void MovePoint()
    {
        float newY = point.anchoredPosition.y + speed * direction * Time.deltaTime;
        point.anchoredPosition = new Vector2(0, newY);

        if(point.anchoredPosition.y >= upLimit)
        {
            direction = -1;
        }
        else if(point.anchoredPosition.y <= downLimit)
        {
            direction = 1;
        }
    }

    public bool WhenPressed()
    {
        float distanceBeetweenPointAndTarget = Mathf.Abs(point.anchoredPosition.y - target.anchoredPosition.y);
        float halfHeight = bar.rect.height / 2f;

        if(distanceBeetweenPointAndTarget <= 1.5f)
        {
            onHit?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }
}
