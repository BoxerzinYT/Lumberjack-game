using System;
using UnityEngine;

public class TreePower_bombArea : MonoBehaviour
{
    public static event Action explodedPlayer;
    float stunTime;
    bool canVerify = true;
    bool verified;
    public void Exploded()
    {
        canVerify = true;
    }

    public void SetArea(float _stunTime)
    {
        stunTime = _stunTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !verified)
        {
            other.GetComponent<Hec_Stun>().StartStun(stunTime);
            explodedPlayer?.Invoke();
            verified = true;
        }
    }
}
