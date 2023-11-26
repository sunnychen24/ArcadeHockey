using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movescale;
    private Animator animator;
    public float shotspeed;
    public enum direction { up, upright, right, downright, down, downleft, left, upleft }
    public direction currentdir = direction.left;
    public bool haspuck = false;

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

    void FixedUpdate(){

        float xMovement = Input.GetAxis("Horizontal2");
        float yMovement = Input.GetAxis("Vertical2");
        Vector2 movement = new Vector2(xMovement, yMovement);
        rb.AddForce(movescale*movement);
        animator.SetInteger("X Input", Mathf.RoundToInt(xMovement));
        animator.SetInteger("Y Input", Mathf.RoundToInt(yMovement));

        if (Input.GetKeyDown(KeyCode.RightShift) && haspuck){
            shoot(movement);
        }
    }
}
