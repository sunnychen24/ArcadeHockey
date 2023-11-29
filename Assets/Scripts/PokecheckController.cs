using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PokecheckController : MonoBehaviour
{
    private float lifetime = 0.833f;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Puck"))
        {
            if (PuckController.fixedJoint.enabled == true)
            {
                if (Random.Range(0, 100) > 33)
                {
                    return;
                }
                else
                {
                    if (PuckController.fixedJoint.connectedBody.gameObject.name.Equals("Player 1"))
                    {
                        PuckController.fixedJoint.connectedBody.gameObject.GetComponent<PlayerController>().haspuck = false;
                    }
                    if (PuckController.fixedJoint.connectedBody.gameObject.name.Equals("Player 2"))
                    {
                        PuckController.fixedJoint.connectedBody.gameObject.GetComponent<Player2Controller>().haspuck = false;
                    }
                    PuckController.fixedJoint.enabled = false;
                    PuckController.fixedJoint.connectedBody = null;
                    PuckController.timesinceshot = 0;
                }
            }
            if (transform.rotation == Quaternion.Euler(0,0,0))
            {
                PuckController.rb.AddForce(new Vector2(-1, -1) * 20);
            }

            else if (transform.rotation == Quaternion.Euler(0, 0, -45))
            {
                PuckController.rb.AddForce(new Vector2(-1, 0) * 20);
            }

            else if (transform.rotation == Quaternion.Euler(0, 0, -90))
            {
                PuckController.rb.AddForce(new Vector2(-1, 1) * 20);
            }

            else if (transform.rotation == Quaternion.Euler(0, 0, -135))
            {
                PuckController.rb.AddForce(new Vector2(0, 1) * 20);
            }

            else if (transform.rotation == Quaternion.Euler(0, 0, -180))
            {
                PuckController.rb.AddForce(new Vector2(1, 1) * 20);
            }

            else if (transform.rotation == Quaternion.Euler(0, 0, -225))
            {
                PuckController.rb.AddForce(new Vector2(1, 0) * 20);
            }

            else if (transform.rotation == Quaternion.Euler(0, 0, -270))
            {
                PuckController.rb.AddForce(new Vector2(1, -1) * 20);
            }

            else if (transform.rotation == Quaternion.Euler(0, 0, -315))
            {
                PuckController.rb.AddForce(new Vector2(0, -1) * 20);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
