using UnityEngine;
using UnityEngine.Events;

public class StructureSystem : MonoBehaviour
{
    [SerializeField] string playerTag;
    [SerializeField] UnityEvent whenPlayerEnter;
    [SerializeField] UnityEvent whenPlayerExit;
    bool playerEnter;

    Hec_Stats lastPlayerThatPassHere;
    public Hec_Stats LastPlayerThatPassHere { get { return lastPlayerThatPassHere; } set { }}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == playerTag && !playerEnter)
        {
            playerEnter = true;
            lastPlayerThatPassHere = other.gameObject.GetComponent<Hec_Stats>();
            if(whenPlayerEnter != null) { whenPlayerEnter.Invoke(); }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == playerTag && playerEnter)
        {
            if(whenPlayerExit != null) { whenPlayerExit.Invoke(); }
            playerEnter = false;
        }
    }
}
