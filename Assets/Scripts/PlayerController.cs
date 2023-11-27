using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movescale;
    private Animator animator;
    public float shotspeed;
    public enum direction {up, upright, right, downright, down, downleft, left, upleft}
    public direction currentdir = direction.right;
    public bool haspuck = false;
    private bool charging = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void shoot(Vector2 direction)
    {
        PuckController.fixedJoint.enabled = false;
        PuckController.fixedJoint.connectedBody = null;
        PuckController.rb.AddForce(shotspeed * direction);
        PuckController.timesinceshot = 0;
        haspuck = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player 2") && charging == true)
        {
            PuckController.fixedJoint.enabled = false;
            PuckController.fixedJoint.connectedBody = null;
            PuckController.timesinceshot = 0;
            collision.gameObject.GetComponent<Player2Controller>().haspuck = false;
        }
    }

    void FixedUpdate(){

        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(xMovement, yMovement);

        if (charging == false)
        {
            rb.AddForce(movescale*movement);
            animator.SetInteger("X Input", Mathf.RoundToInt(xMovement));
            animator.SetInteger("Y Input", Mathf.RoundToInt(yMovement));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && haspuck ){
            shoot(movement);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.drag = 0;
            charging = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            rb.drag = 0.9f;
            charging = false;
        }

        if (xMovement != 0)
        {
            if (xMovement > 0)
            {
                if (yMovement > 0)
                {
                    currentdir = direction.upright;
                }
                else if (yMovement < 0) { currentdir = direction.downright; }
                else { currentdir = direction.right; }
            }
            else
            {
                if (yMovement > 0)
                {
                    currentdir = direction.upleft;
                }
                else if (yMovement < 0) { currentdir = direction.downleft; }
                else { currentdir = direction.left; }
            }
        }

        if (xMovement == 0 && yMovement != 0)
        {
            if (yMovement > 0)
            {
                currentdir = direction.up;
            }
            else
            {
                currentdir = direction.down;
            }
        }
    }
}
