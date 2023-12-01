using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuckController : MonoBehaviour
{

    public static FixedJoint2D fixedJoint;
    public static Rigidbody2D rb;
    public static float timesinceshot = 0;
    public PlayerController player1;
    public Player2Controller player2;
    public AiController ai;
    public GameController controller;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        fixedJoint = this.gameObject.GetComponent<FixedJoint2D>();
        fixedJoint.enabled = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timesinceshot += Time.deltaTime;
        if (fixedJoint.connectedBody != null)
        {
            if (player1.haspuck)
            {
                if (player1.currentdir==PlayerController.direction.right)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.5f, -0.8f, 0f), 0.06f);

                else if (player1.currentdir == PlayerController.direction.upright)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(1.0f, -0.2f, 0f), 0.06f);

                else if (player1.currentdir == PlayerController.direction.up)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.8f, 0.5f, 0f), 0.06f);

                else if (player1.currentdir == PlayerController.direction.upleft)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.2f, 1.0f, 0f), 0.06f);

                else if (player1.currentdir == PlayerController.direction.left)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.5f, 0.8f, 0f), 0.06f);

                else if (player1.currentdir == PlayerController.direction.downleft)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-1.0f, 0.2f, 0f), 0.06f);

                else if (player1.currentdir == PlayerController.direction.down)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.8f, -0.5f, 0f), 0.06f);

                else if (player1.currentdir == PlayerController.direction.downright)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.2f, -1.0f, 0f), 0.06f);
            }
            else if (player2.haspuck)
            {
                if (player2.currentdir == Player2Controller.direction.right)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.5f, -0.8f, 0f), 0.06f);

                else if (player2.currentdir == Player2Controller.direction.upright)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(1.0f, -0.2f, 0f), 0.06f);

                else if (player2.currentdir == Player2Controller.direction.up)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.8f, 0.5f, 0f), 0.06f);

                else if (player2.currentdir == Player2Controller.direction.upleft)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.2f, 1.0f, 0f), 0.06f);

                else if (player2.currentdir == Player2Controller.direction.left)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.5f, 0.8f, 0f), 0.06f);

                else if (player2.currentdir == Player2Controller.direction.downleft)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-1.0f, 0.2f, 0f), 0.06f);

                else if (player2.currentdir == Player2Controller.direction.down)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.8f, -0.5f, 0f), 0.06f);

                else if (player2.currentdir == Player2Controller.direction.downright)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.2f, -1.0f, 0f), 0.06f);
            }
            else if (ai.haspuck)
            {
                if (ai.currentdir == AiController.direction.right)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.5f, -0.8f, 0f), 0.06f);

                else if (ai.currentdir == AiController.direction.upright)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(1.0f, -0.2f, 0f), 0.06f);

                else if (ai.currentdir == AiController.direction.up)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.8f, 0.5f, 0f), 0.06f);

                else if (ai.currentdir == AiController.direction.upleft)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(0.2f, 1.0f, 0f), 0.06f);

                else if (ai.currentdir == AiController.direction.left)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.5f, 0.8f, 0f), 0.06f);

                else if (ai.currentdir == AiController.direction.downleft)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-1.0f, 0.2f, 0f), 0.06f);

                else if (ai.currentdir == AiController.direction.down)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.8f, -0.5f, 0f), 0.06f);

                else if (ai.currentdir == AiController.direction.downright)
                    transform.position = Vector3.Lerp(transform.position, fixedJoint.connectedBody.transform.position + new Vector3(-0.2f, -1.0f, 0f), 0.06f);
            }
        }
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

        if (!collision.gameObject.name.Equals("Ice Rink") && timesinceshot>1.0f && fixedJoint.connectedBody == null) {
            fixedJoint.enabled = true;
            fixedJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            timesinceshot = 0;
            if (collision.gameObject.name.Equals("Player 1"))
            {
                player1 = collision.gameObject.GetComponent<PlayerController>();
                player1.haspuck = true;
                player2.haspuck = false;
                ai.haspuck = false;
            }
            else if (collision.gameObject.name.Equals("Player 2"))
            {
                player2 = collision.gameObject.GetComponent<Player2Controller>();
                player2.haspuck = true;
                player1.haspuck = false;
                ai.haspuck = false;
            }
            else if (collision.gameObject.name.Equals("AI"))
            {
                ai = collision.gameObject.GetComponent<AiController>();
                ai.haspuck = true;
                player1.haspuck = false;
                player2.haspuck = false;
            }
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
