using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{
    public static FixedJoint2D fixedJoint;
    public static Rigidbody2D rb;
    public static float timesinceshot = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        fixedJoint = this.gameObject.GetComponent<FixedJoint2D>();

    }

    // Update is called once per frame
    void Update()
    {
        timesinceshot += Time.deltaTime;
        if (fixedJoint.connectedBody != null)
        {
            //if (fixedJoint.connectedBody.gameObject.GetComponent<PlayerController>().currentdir))
            //transform.position = fixedJoint.connectedBody.transform.position +
                //new Vector3(0.5f, -0.8f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.name.Equals("Ice Rink") && timesinceshot>1.0f) {
            fixedJoint.enabled = true;
            fixedJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (collision.gameObject.name.Equals("Player 1"))
            {
                collision.gameObject.GetComponent<PlayerController>().haspuck = true;
            }
            if (collision.gameObject.name.Equals("Player 2"))
            {
                collision.gameObject.GetComponent<Player2Controller>().haspuck = true;
            }
        }
    }
}
