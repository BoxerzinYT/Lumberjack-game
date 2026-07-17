using System.Collections;
using UnityEngine;

public class Hec_Stun : MonoBehaviour
{
    Hec_passive_dash myDashPower;
    Animator anim;
    float stunDuration;

    public void Start()
    {
        anim = GetComponent<Animator>();
        myDashPower = GetComponent<Hec_passive_dash>();
    }


    public void StartStun(float _stunDuration)
    {
        stunDuration = _stunDuration;
        StartCoroutine(Stunned());
    }

    public IEnumerator Stunned()
    {
        EventsManager.eventM.playerCanInteract = false;
        EventsManager.eventM.playerCanWalk = false;
        myDashPower.GlobalCanDash = false;

        anim.SetTrigger("StartStun");
        anim.SetBool("Stunned", true);

        yield return new WaitForSeconds(stunDuration);

        anim.SetBool("Stunned", false);

        EventsManager.eventM.playerCanInteract = true;
        EventsManager.eventM.playerCanWalk = true;
        myDashPower.GlobalCanDash = true;

        StopCoroutine("Stunned");
    }
}
