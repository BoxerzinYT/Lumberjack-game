using UnityEngine;
using UnityEngine.Events;

public class StructureSystem : Qol_BuySystem
{
    string playerTag = "Player";
    [SerializeField] GameObject ClickButtonHUD;
    [SerializeField] UnityEvent whenPlayerEnter;
    [SerializeField] UnityEvent whenPlayerClick;
    [SerializeField] UnityEvent whenPlayerExit;
    bool playerEnter;
    bool clicked;

    Hec_Stats lastPlayerThatPassHere;
    public Hec_Stats LastPlayerThatPassHere { get { return lastPlayerThatPassHere; } set { }}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == playerTag && !playerEnter)
        {
            playerEnter = true;
            clicked = false;
            lastPlayerThatPassHere = other.gameObject.GetComponent<Hec_Stats>();
            whenPlayerEnter?.Invoke();
            ClickButtonHUD.SetActive(true);

            //Debug.Log("Clicked? " + clicked);
            //Debug.Log("Entered? " + playerEnter);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == playerTag && playerEnter)
        {
            whenPlayerExit?.Invoke();
            playerEnter = false;
            ClickButtonHUD.SetActive(false);
            //Debug.Log("Clicked? " + clicked);
            //Debug.Log("Entered? " + playerEnter);
        }
    }

    public void Update()
    {
        if (playerEnter && !clicked && Input.GetKeyDown(KeyCode.E))
        {
            ClickButtonHUD.SetActive(false);
            //Debug.Log("Clicked");
            whenPlayerClick?.Invoke();
            clicked = true;
        }
    }
}
