using UnityEngine;

public class UI_NotificationCanvas : MonoBehaviour
{
    [SerializeField] Transform notiContent;
    public Transform NotiContent { get { return notiContent; } }

    //bool doesntHaveNotis = false;
    //public bool DoesntHaveNotis { get { return doesntHaveNotis; } set { doesntHaveNotis = value; } }
    //float timeToDestroy = 3;
    //float elapsedTime;

    /*
    public void Update()
    {
        if(notiContent.childCount <= 0 && !doesntHaveNotis)
        {
            elapsedTime = timeToDestroy;
            Debug.Log("I will destroy myself");
            doesntHaveNotis = true;
        }
        if(doesntHaveNotis && elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            if(elapsedTime <= 0)
            {
                Debug.Log("Destroyed");
                Destroy(gameObject);
            }
        }
    }
    */
}
