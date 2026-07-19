using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Hec_Mov : MonoBehaviour
{
    Hec_Stats hj_stats;
    Rigidbody2D rb;
    Animator anim;
    Hec_passive_dash myDashPower;
    Vector2 moveInput;

    public void Awake()
    {
        hj_stats = GetComponent<Hec_Stats>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myDashPower = GetComponent<Hec_passive_dash>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void FixedUpdate()
    {
        if (!myDashPower.IsDashing && EventsManager.eventM.playerCanWalk)
        {
            rb.linearVelocity = moveInput.normalized * hj_stats.hec_speed * hj_stats.Hec_speedMult;
        }
        else if (!EventsManager.eventM.playerCanWalk)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    public void LateUpdate()
    {
        if (EventsManager.eventM.playerCanWalk)
        {
            FlipX(moveInput.x);
            anim.SetBool("Walking", moveInput.magnitude > 0);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    public void FlipX(float x)
    {
        if(x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
        }
    }
}
