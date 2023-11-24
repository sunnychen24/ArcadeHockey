using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movescale;
    private Animator animator;
    
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
    void FixedUpdate(){

        float xMovement = Input.GetAxis("Horizontal2");
        float yMovement = Input.GetAxis("Vertical2");
        Vector2 movement = new Vector2(xMovement, yMovement);
        rb.AddForce(movescale*movement);
        animator.SetInteger("X Input", Mathf.RoundToInt(xMovement));
        animator.SetInteger("Y Input", Mathf.RoundToInt(yMovement));
    }
}
