using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuckController : MonoBehaviour
{
    public FixedJoint2D fixedJoint;
    public GameController controller;
    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Equals("Ice Rink")) {
            fixedJoint = this.gameObject.GetComponent<FixedJoint2D>();
            fixedJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player 1 Net"))
        {
            if (collision.bounds.Contains(GetComponent<Collider2D>().bounds.min) && collision.bounds.Contains(GetComponent <Collider2D>().bounds.max))
            {
                StartCoroutine(controller.GoalScored(1));
            }
        } else if (collision.gameObject.name.Equals("Player 2 Net"))
        {
            if (collision.bounds.Contains(GetComponent<Collider2D>().bounds.min) && collision.bounds.Contains(GetComponent<Collider2D>().bounds.max))
            {
                StartCoroutine(controller.GoalScored(0));
            }
        }
    }
}
