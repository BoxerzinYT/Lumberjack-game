using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Hec_Mov : MonoBehaviour
{
    Hec_Stats hj_stats;
    Rigidbody2D rb;
    Animator anim;

    public void Awake()
    {
        hj_stats = GetComponent<Hec_Stats>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        if (EventsManager.eventM.playerCanWalk)
        {
            var moveInput = value.Get<Vector2>();
            FlipX(moveInput.x);
            rb.linearVelocity = moveInput.normalized * hj_stats.hec_speed;
            anim.SetBool("Walking", rb.linearVelocity.magnitude > 0);
        }
        else
        {
            anim.SetBool("Walking", false);
            rb.linearVelocity = new Vector2(0, 0);
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
