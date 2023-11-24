using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{
    public FixedJoint2D fixedJoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        fixedJoint = this.gameObject.GetComponent<FixedJoint2D>();
        fixedJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
    }
}
