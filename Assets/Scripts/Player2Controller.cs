using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movescale;
    private Animator animator;
    public float shotspeed;
    public enum direction {up, upright, right, downright, down, downleft, left, upleft }
    public direction currentdir = direction.left;
    public bool haspuck = false;
    private bool charging = false;
    public float stuntimer;
    public PlayerController player1;
    public GameObject pokecheck;
    private Vector2 movement = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && haspuck)
        {
            shoot(movement);
        }

        if (Input.GetKeyDown(KeyCode.O) && !haspuck)
        {
            rb.drag = 0;
            charging = true;
        }

        if (Input.GetKeyUp(KeyCode.O) || haspuck)
        {
            rb.drag = 0.9f;
            charging = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket) && haspuck == false)
        {
            if (currentdir == direction.downleft)
                Instantiate(pokecheck, transform.position + new Vector3(-0.68f, -0.68f, 0), Quaternion.Euler(0, 0, 0));

            else if (currentdir == direction.left)
                Instantiate(pokecheck, transform.position + new Vector3(-0.97f, 0, 0), Quaternion.Euler(0, 0, -45));

            else if (currentdir == direction.upleft)
                Instantiate(pokecheck, transform.position + new Vector3(-0.68f, 0.68f, 0), Quaternion.Euler(0, 0, -90));

            else if (currentdir == direction.up)
                Instantiate(pokecheck, transform.position + new Vector3(0, 0.97f, 0), Quaternion.Euler(0, 0, -135));

            else if (currentdir == direction.upright)
                Instantiate(pokecheck, transform.position + new Vector3(0.68f, 0.68f, 0), Quaternion.Euler(0, 0, -180));

            else if (currentdir == direction.right)
                Instantiate(pokecheck, transform.position + new Vector3(0.97f, 0, 0), Quaternion.Euler(0, 0, -225));

            else if (currentdir == direction.downright)
                Instantiate(pokecheck, transform.position + new Vector3(0.68f, -0.68f, 0), Quaternion.Euler(0, 0, -270));

            else if (currentdir == direction.down)
                Instantiate(pokecheck, transform.position + new Vector3(0, -0.97f, 0), Quaternion.Euler(0, 0, -315));
        }
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
        if (collision.gameObject.name.Equals("Player 1") && charging == true)
        {
            player1 = collision.gameObject.GetComponent<PlayerController>();
            PuckController.fixedJoint.enabled = false;
            PuckController.fixedJoint.connectedBody = null;
            PuckController.timesinceshot = 0;
            player1.stuntimer = 2.0f;

            if (player1.haspuck)
            {
                if (player1.currentdir == PlayerController.direction.left)
                    PuckController.rb.AddForce(new Vector2(-1, 0) * 20);

                else if (player1.currentdir == PlayerController.direction.upleft)
                    PuckController.rb.AddForce(new Vector2(-1, 1) * 20);

                else if (player1.currentdir == PlayerController.direction.up)
                    PuckController.rb.AddForce(new Vector2(0, 1) * 20);

                else if (player1.currentdir == PlayerController.direction.upright)
                    PuckController.rb.AddForce(new Vector2(1, 1) * 20);

                else if (player1.currentdir == PlayerController.direction.right)
                    PuckController.rb.AddForce(new Vector2(1, 0) * 20);

                else if (player1.currentdir == PlayerController.direction.downright)
                    PuckController.rb.AddForce(new Vector2(1, -1) * 20);

                else if (player1.currentdir == PlayerController.direction.down)
                    PuckController.rb.AddForce(new Vector2(0, -1) * 20);

                else if (player1.currentdir == PlayerController.direction.downleft)
                    PuckController.rb.AddForce(new Vector2(-1, -1) * 20);
            }

            player1.haspuck = false;
        }
    }

    void FixedUpdate(){

        float xMovement = Input.GetAxis("Horizontal2");
        float yMovement = Input.GetAxis("Vertical2");
        movement = new Vector2(xMovement, yMovement);

        if (charging == false && stuntimer <= 0)
        {
            rb.AddForce(movescale * movement);
            animator.SetInteger("X Input", Mathf.RoundToInt(xMovement));
            animator.SetInteger("Y Input", Mathf.RoundToInt(yMovement));
        }

        if (stuntimer > 0)
        {
            stuntimer -= Time.deltaTime;
            if (stuntimer <= 0)
                GetComponent<SpriteRenderer>().enabled = true;
            else if (stuntimer < 0.33f)
                GetComponent<SpriteRenderer>().enabled = false;
            else if (stuntimer < 0.66f)
                GetComponent<SpriteRenderer>().enabled = true;
            else if (stuntimer < 1.0f)
                GetComponent<SpriteRenderer>().enabled = false;
            else if (stuntimer < 1.33f)
                GetComponent<SpriteRenderer>().enabled = true;
            else if (stuntimer < 1.66f)
                GetComponent<SpriteRenderer>().enabled = false;
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
