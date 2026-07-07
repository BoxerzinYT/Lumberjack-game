using System;
using System.Collections;
using UnityEngine;

public interface IBreakable
{
    public void Break(Hec_Stats hec_stats);
}

public class Hec_UseAxe : MonoBehaviour
{
    bool canAttack;
    [SerializeField] float timeBeetweenAttacks;
    Animator anim;

    [Header("Break_Breakables")]
    [SerializeField] Hec_Stats myStats;
    [SerializeField] LayerMask breakablesLayer;

    public void Start()
    {
        StartCoroutine(CooldownForAttackAgain());
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (EventsManager.eventM.playerCanInteract)
        {
            if(canAttack && Input.GetKey(KeyCode.Mouse0))
            {
                canAttack = false;
                anim.SetTrigger("Use");
                StartCoroutine(CooldownForAttackAgain());

            }
        }
    }

    IEnumerator CooldownForAttackAgain()
    {
        yield return new WaitForSeconds(timeBeetweenAttacks);
        foreach(var b in GetTheBreakables())
        {
            if (b.GetComponent<BreakableObj>())
            {
                b.GetComponent<IBreakable>().Break(myStats);
            }
        }
        canAttack = true;
        StopCoroutine("CooldownForAttackAgain");
    }

    public Collider2D[] GetTheBreakables()
    {
        Collider2D[] breakables = Physics2D.OverlapCircleAll(transform.position, myStats.hec_range, breakablesLayer);
        return breakables;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, myStats.hec_range);
    }
}
