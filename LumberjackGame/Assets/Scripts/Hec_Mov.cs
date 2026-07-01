using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Hec_Mov : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    Animator anim;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        var moveInput = value.Get<Vector2>();
        FlipX(moveInput.x);
        rb.linearVelocity = moveInput.normalized * speed;
        //anim.SetBool("Moving", rb.linearVelocity.magnitude > 0);
    }

    public void FlipX(float x)
    {
        if(x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
        }
    }
}
