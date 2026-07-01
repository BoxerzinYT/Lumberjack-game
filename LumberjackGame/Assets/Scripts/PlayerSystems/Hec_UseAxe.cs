using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IBreakable
{
    public void Break(Breakable_Interactions inter);
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
        if(canAttack && Input.GetKey(KeyCode.Mouse0))
        {
            canAttack = false;
            anim.SetTrigger("Use");
            StartCoroutine(CooldownForAttackAgain());
        }
    }

    IEnumerator CooldownForAttackAgain()
    {
        yield return new WaitForSeconds(timeBeetweenAttacks);
        foreach(var b in GetTheBreakables())
        {
            if (b.GetComponent<BreakableObj>())
            {
                b.GetComponent<IBreakable>().Break(new Breakable_Interactions
                (myStats.hec_damage, myStats.hec_criticalChance, myStats.hec_bonusChance, myStats.hec_dropPoints));
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
