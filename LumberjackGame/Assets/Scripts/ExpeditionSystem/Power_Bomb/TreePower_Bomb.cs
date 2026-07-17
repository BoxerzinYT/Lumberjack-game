using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class TreePower_Bomb : MonoBehaviour
{
    [SerializeField] BombType bombType;
    [SerializeField] float timeToExplode;
    [SerializeField] float stunTime;
    float elapsedTime;
    bool canCooldownToExplode = false;
    bool exploded = false;
    [SerializeField] Gradient bombTimerGradient;

    [SerializeField] SpriteRenderer mybombAreaSp;
    [SerializeField] TreePower_bombArea myBombArea;
    [SerializeField] float rotateSpeed;
    Animator anim;
    bool canRotate;

    public void Start()
    {
        anim = GetComponent<Animator>();
        myBombArea.SetArea(stunTime);
        myBombArea.gameObject.SetActive(false);
        elapsedTime = timeToExplode;
        canCooldownToExplode = true;
        float randomNumberForAngle = Random.Range(0, 360);
        switch (bombType)
        {
            case BombType.normal:
            break;
            case BombType.withSpace:
            mybombAreaSp.transform.eulerAngles = new Vector3(0,0,randomNumberForAngle);
            myBombArea.transform.eulerAngles = new Vector3(0,0,randomNumberForAngle);
            break;
            case BombType.withSpaceAndRotate:
            mybombAreaSp.transform.eulerAngles = new Vector3(0,0,randomNumberForAngle);
            myBombArea.transform.eulerAngles = new Vector3(0,0,randomNumberForAngle);
            canRotate = true;
            break;
        }
    }

    public void Update()
    {
        if(elapsedTime > 0 && canCooldownToExplode && !exploded)
        {
            if(canRotate)
            {
                mybombAreaSp.transform.eulerAngles = mybombAreaSp.transform.eulerAngles + new Vector3(0,0, rotateSpeed * Time.deltaTime);
                myBombArea.transform.eulerAngles = myBombArea.transform.eulerAngles + new Vector3(0,0, rotateSpeed * Time.deltaTime);
            }
            elapsedTime -= Time.deltaTime;
            mybombAreaSp.color = bombTimerGradient.Evaluate(elapsedTime / timeToExplode);
            if(elapsedTime <= 0)
            {
                canRotate = false;
                anim.SetTrigger("Explosion");
                //myBombArea.Exploded();
                myBombArea.gameObject.SetActive(true);
                Destroy(myBombArea.gameObject, 0.075f);
                exploded = true;
                Destroy(this.gameObject, 1.5f);
                Destroy(mybombAreaSp.gameObject);
            }
        }
    }
}
[System.Serializable]
public enum BombType
{
    normal,
    withSpace,
    withSpaceAndRotate
}
