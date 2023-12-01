using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float movescale;
    private float shotspeed;
    private float stuntimer = 0;
    private float pokecheckCD = 0;
    private float chargetimer = 0;
    public enum direction { up, upright, right, downright, down, downleft, left, upleft }
    public direction currentdir = direction.left;
    public enum AIState { Puck, Player, Score, FindLane, Goon }
    public AIState state = AIState.Puck;
    public bool haspuck = false;
    private bool charging = false;
    public GameObject player;
    private PlayerController player1;
    public GameObject puck;
    public GameObject pokecheck;
    public List<AiStats> statsList;
    private AiStats stats;
    private Vector2 skateDir = Vector2.zero;
    private float skateTimer = 0;
    private float shootCD = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player1 = player.GetComponent<PlayerController>();

        stats = statsList[PlayerPrefs.GetInt("1")];

        if (stats.isGoon)
        {
            state = AIState.Goon;
        }

        movescale = stats.movespeed;
        shotspeed = stats.shotpower;
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

    public void Stun()
    {
        if (stuntimer <= 0)
        {
            stuntimer = stats.stunduration;
        }
    }

    public void ResetState()
    {
        if (state != AIState.Goon)
        {
            state = AIState.Puck;
        }
    }

    private void FixedUpdate()
    {
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

        if (state == AIState.Puck)
        {
            if (stuntimer <= 0)
            {
                Vector3 dir3D = puck.transform.position - transform.position;
                Vector2 dir2D = new Vector2(dir3D.x, dir3D.y);
                Vector2 dir = Vector2.zero;

                if (Mathf.Sign(dir2D.x) == 1 && Mathf.Sign(dir2D.y) == 1)
                {
                    if (Vector2.Angle(Vector2.right, dir2D) > 30)
                    {
                        dir.y = 1;
                    }

                    if (Vector2.Angle(Vector2.up, dir2D) > 30)
                    {
                        dir.x = 1;
                    }
                }
                else if (Mathf.Sign(dir2D.x) == 1 && Mathf.Sign(dir2D.y) == -1)
                {
                    if (Vector2.Angle(Vector2.right, dir2D) > 30)
                    {
                        dir.y = -1;
                    }

                    if (Vector2.Angle(Vector2.down, dir2D) > 30)
                    {
                        dir.x = 1;
                    }
                }
                else if (Mathf.Sign(dir2D.x) == -1 && Mathf.Sign(dir2D.y) == -1)
                {
                    if (Vector2.Angle(Vector2.left, dir2D) > 30)
                    {
                        dir.y = -1;
                    }

                    if (Vector2.Angle(Vector2.down, dir2D) > 30)
                    {
                        dir.x = -1;
                    }
                }
                else
                {
                    if (Vector2.Angle(Vector2.left, dir2D) > 30)
                    {
                        dir.y = 1;
                    }

                    if (Vector2.Angle(Vector2.up, dir2D) > 30)
                    {
                        dir.x = -1;
                    }
                }

                animator.SetInteger("X Input", (int)dir.x);
                animator.SetInteger("Y Input", (int)dir.y);


                if (dir.x != 0)
                {
                    if (dir.x > 0)
                    {
                        if (dir.y > 0)
                        {
                            currentdir = direction.upright;
                        }
                        else if (dir.y < 0) { currentdir = direction.downright; }
                        else { currentdir = direction.right; }
                    }
                    else
                    {
                        if (dir.y > 0)
                        {
                            currentdir = direction.upleft;
                        }
                        else if (dir.y < 0) { currentdir = direction.downleft; }
                        else { currentdir = direction.left; }
                    }
                }

                if (dir.x == 0 && dir.y != 0)
                {
                    if (dir.y > 0)
                    {
                        currentdir = direction.up;
                    }
                    else
                    {
                        currentdir = direction.down;
                    }
                }

                rb.AddForce(movescale * new Vector2(dir.x, dir.y).normalized);
            }

            if (player1.haspuck)
            {
                state = AIState.Player;
            }
            else if (haspuck)
            {
                state = AIState.Score;
                shootCD = 0.75f;
            }
        }
        else if (state == AIState.Player)
        {
            if (stuntimer <= 0)
            {
                Vector3 dir3D = player.transform.position - transform.position;
                Vector2 dir2D = new Vector2(dir3D.x, dir3D.y);
                Vector2 dir = Vector2.zero;

                if (Mathf.Sign(dir2D.x) == 1 && Mathf.Sign(dir2D.y) == 1)
                {
                    if (Vector2.Angle(Vector2.right, dir2D) > 30)
                    {
                        dir.y = 1;
                    }

                    if (Vector2.Angle(Vector2.up, dir2D) > 30)
                    {
                        dir.x = 1;
                    }
                }
                else if (Mathf.Sign(dir2D.x) == 1 && Mathf.Sign(dir2D.y) == -1)
                {
                    if (Vector2.Angle(Vector2.right, dir2D) > 30)
                    {
                        dir.y = -1;
                    }

                    if (Vector2.Angle(Vector2.down, dir2D) > 30)
                    {
                        dir.x = 1;
                    }
                }
                else if (Mathf.Sign(dir2D.x) == -1 && Mathf.Sign(dir2D.y) == -1)
                {
                    if (Vector2.Angle(Vector2.left, dir2D) > 30)
                    {
                        dir.y = -1;
                    }

                    if (Vector2.Angle(Vector2.down, dir2D) > 30)
                    {
                        dir.x = -1;
                    }
                }
                else
                {
                    if (Vector2.Angle(Vector2.left, dir2D) > 30)
                    {
                        dir.y = 1;
                    }

                    if (Vector2.Angle(Vector2.up, dir2D) > 30)
                    {
                        dir.x = -1;
                    }
                }

                if (pokecheckCD > 0)
                {
                    pokecheckCD -= Time.deltaTime;
                }

                if (dir3D.magnitude < 4 && chargetimer <= 0 && Vector2.Angle(rb.velocity, dir) < 20 && player1.stuntimer <= 0 && Random.Range(0, 100) >= 75)
                {
                    chargetimer = 1f;
                }

                if (chargetimer > 0)
                {
                    rb.drag = 0;
                    charging = true;
                    chargetimer -= Time.deltaTime;
                }
                else
                {
                    rb.drag = 0.9f;
                    charging = false;

                    animator.SetInteger("X Input", (int)dir.x);
                    animator.SetInteger("Y Input", (int)dir.y);

                    if (dir.x != 0)
                    {
                        if (dir.x > 0)
                        {
                            if (dir.y > 0)
                            {
                                currentdir = direction.upright;
                            }
                            else if (dir.y < 0) { currentdir = direction.downright; }
                            else { currentdir = direction.right; }
                        }
                        else
                        {
                            if (dir.y > 0)
                            {
                                currentdir = direction.upleft;
                            }
                            else if (dir.y < 0) { currentdir = direction.downleft; }
                            else { currentdir = direction.left; }
                        }
                    }

                    if (dir.x == 0 && dir.y != 0)
                    {
                        if (dir.y > 0)
                        {
                            currentdir = direction.up;
                        }
                        else
                        {
                            currentdir = direction.down;
                        }
                    }

                    rb.AddForce(movescale * dir.normalized);

                    if (dir3D.magnitude < 3 && Vector2.Angle(rb.velocity, dir) < 20 && pokecheckCD <= 0)
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

                        pokecheckCD = stats.pokecheck;
                    }
                }
            }

            if (!player1.haspuck)
            {
                state = AIState.Puck;
                chargetimer = 0;
                rb.drag = 0.9f;
                charging = false;
            }
        }
        else if (state == AIState.Score)
        {
            if (stuntimer <= 0)
            {
                Vector2 dir2D = new Vector2(-7.5f, 0) - new Vector2(transform.position.x, transform.position.y);
                Vector2 dir = Vector2.zero;

                if (shootCD > 0)
                {
                    shootCD -= Time.deltaTime;
                }

                if (Mathf.Sign(dir2D.x) == 1 && Mathf.Sign(dir2D.y) == 1)
                {
                    if (Vector2.Angle(Vector2.right, dir2D) > 30)
                    {
                        dir.y = 1;
                    }

                    if (Vector2.Angle(Vector2.up, dir2D) > 30)
                    {
                        dir.x = 1;
                    }
                }
                else if (Mathf.Sign(dir2D.x) == 1 && Mathf.Sign(dir2D.y) == -1)
                {
                    if (Vector2.Angle(Vector2.right, dir2D) > 30)
                    {
                        dir.y = -1;
                    }

                    if (Vector2.Angle(Vector2.down, dir2D) > 30)
                    {
                        dir.x = 1;
                    }
                }
                else if (Mathf.Sign(dir2D.x) == -1 && Mathf.Sign(dir2D.y) == -1)
                {
                    if (Vector2.Angle(Vector2.left, dir2D) > 30)
                    {
                        dir.y = -1;
                    }

                    if (Vector2.Angle(Vector2.down, dir2D) > 30)
                    {
                        dir.x = -1;
                    }
                }
                else
                {
                    if (Vector2.Angle(Vector2.left, dir2D) > 30)
                    {
                        dir.y = 1;
                    }

                    if (Vector2.Angle(Vector2.up, dir2D) > 30)
                    {
                        dir.x = -1;
                    }
                }

                animator.SetInteger("X Input", (int)dir.x);
                animator.SetInteger("Y Input", (int)dir.y);

                if (dir.x != 0)
                {
                    if (dir.x > 0)
                    {
                        if (dir.y > 0)
                        {
                            currentdir = direction.upright;
                        }
                        else if (dir.y < 0) { currentdir = direction.downright; }
                        else { currentdir = direction.right; }
                    }
                    else
                    {
                        if (dir.y > 0)
                        {
                            currentdir = direction.upleft;
                        }
                        else if (dir.y < 0) { currentdir = direction.downleft; }
                        else { currentdir = direction.left; }
                    }
                }

                if (dir.x == 0 && dir.y != 0)
                {
                    if (dir.y > 0)
                    {
                        currentdir = direction.up;
                    }
                    else
                    {
                        currentdir = direction.down;
                    }
                }

                rb.AddForce(movescale * dir.normalized);

                if (dir.magnitude < 7 && shootCD <= 0)
                {
                    List<RaycastHit2D> hit = new List<RaycastHit2D>();
                    Vector2 shootDir = Vector2.zero;

                    if (currentdir == direction.downleft)
                    {
                        Physics2D.Raycast(transform.position, new Vector2(-1, -1), new ContactFilter2D().NoFilter(), hit);
                        shootDir = new Vector2(-1, -1);

                        if (Random.Range(0, 100) < 50)
                        {
                            skateDir = new Vector2(-1, 0);
                        }
                        else
                        {
                            skateDir = new Vector2(0, -1);
                        }
                    }
                    else if (currentdir == direction.left)
                    {
                        Physics2D.Raycast(transform.position, new Vector2(-1, 0), new ContactFilter2D().NoFilter(), hit);
                        shootDir = new Vector2(-1, 0);

                        int rng = Random.Range(0, 100);
                        if (rng < 25)
                        {
                            skateDir = new Vector2(0, 1);
                        }
                        else if (rng >= 25 && rng < 50)
                        {
                            skateDir = new Vector2(-1, 1);
                        }
                        else if (rng >= 50 && rng < 75)
                        {
                            skateDir = new Vector2(-1, -1);
                        }
                        else
                        {
                            skateDir = new Vector2(0, -1);
                        }
                    }
                    else if (currentdir == direction.upleft)
                    {
                        Physics2D.Raycast(transform.position, new Vector2(-1, 1), new ContactFilter2D().NoFilter(), hit);
                        shootDir = new Vector2(-1, 1);

                        if (Random.Range(0, 100) < 50)
                        {
                            skateDir = new Vector2(-1, 0);
                        }
                        else
                        {
                            skateDir = new Vector2(0, 1);
                        }
                    }

                    bool canShoot = false;
                    bool isBlocked = false;
                    for (int i = 0; i < hit.Count; i++)
                    {
                        if (hit[i].collider.name.Equals("Player 1"))
                        {
                            isBlocked = true;
                        }

                        if (hit[i].collider.name.Equals("Player 1 Net"))
                        {
                            canShoot = true;
                        }
                    }

                    if (canShoot && !isBlocked)
                    {
                        shoot(shootDir);
                    }
                    else if (canShoot && isBlocked)
                    {
                        state = AIState.FindLane;
                        skateTimer = 1f;
                    }
                }
            }

            if (!haspuck)
            {
                state = AIState.Puck;
            }
        }
        else if (state == AIState.FindLane)
        {
            skateTimer -= Time.deltaTime;

            if (skateTimer > 0 && haspuck)
            {
                animator.SetInteger("X Input", (int)skateDir.x);
                animator.SetInteger("Y Input", (int)skateDir.y);

                if (skateDir.x != 0)
                {
                    if (skateDir.x > 0)
                    {
                        if (skateDir.y > 0)
                        {
                            currentdir = direction.upright;
                        }
                        else if (skateDir.y < 0) { currentdir = direction.downright; }
                        else { currentdir = direction.right; }
                    }
                    else
                    {
                        if (skateDir.y > 0)
                        {
                            currentdir = direction.upleft;
                        }
                        else if (skateDir.y < 0) { currentdir = direction.downleft; }
                        else { currentdir = direction.left; }
                    }
                }

                if (skateDir.x == 0 && skateDir.y != 0)
                {
                    if (skateDir.y > 0)
                    {
                        currentdir = direction.up;
                    }
                    else
                    {
                        currentdir = direction.down;
                    }
                }

                rb.AddForce(movescale * skateDir.normalized);
            }
            
            if (skateTimer <= 0 && haspuck)
            {
                state = AIState.Score;
            }

            if (!haspuck)
            {
                state = AIState.Puck;
            }
        }
        else
        {
            if (stuntimer <= 0)
            {
                Vector3 dir3D = player.transform.position - transform.position;
                Vector2 dir2D = new Vector2(dir3D.x, dir3D.y);
                Vector2 dir = Vector2.zero;

                if (haspuck)
                {
                    shoot(new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)));
                }

                if (Mathf.Sign(dir2D.x) == 1 && Mathf.Sign(dir2D.y) == 1)
                {
                    if (Vector2.Angle(Vector2.right, dir2D) > 30)
                    {
                        dir.y = 1;
                    }

                    if (Vector2.Angle(Vector2.up, dir2D) > 30)
                    {
                        dir.x = 1;
                    }
                }
                else if (Mathf.Sign(dir2D.x) == 1 && Mathf.Sign(dir2D.y) == -1)
                {
                    if (Vector2.Angle(Vector2.right, dir2D) > 30)
                    {
                        dir.y = -1;
                    }

                    if (Vector2.Angle(Vector2.down, dir2D) > 30)
                    {
                        dir.x = 1;
                    }
                }
                else if (Mathf.Sign(dir2D.x) == -1 && Mathf.Sign(dir2D.y) == -1)
                {
                    if (Vector2.Angle(Vector2.left, dir2D) > 30)
                    {
                        dir.y = -1;
                    }

                    if (Vector2.Angle(Vector2.down, dir2D) > 30)
                    {
                        dir.x = -1;
                    }
                }
                else
                {
                    if (Vector2.Angle(Vector2.left, dir2D) > 30)
                    {
                        dir.y = 1;
                    }

                    if (Vector2.Angle(Vector2.up, dir2D) > 30)
                    {
                        dir.x = -1;
                    }
                }

                if (dir3D.magnitude < 4 && chargetimer <= 0 && Vector2.Angle(rb.velocity, dir) < 20 && player1.stuntimer <= 0)
                {
                    chargetimer = 1f;
                }

                if (chargetimer > 0)
                {
                    rb.drag = 0;
                    charging = true;
                    chargetimer -= Time.deltaTime;
                }
                else
                {
                    rb.drag = 0.9f;
                    charging = false;

                    animator.SetInteger("X Input", (int)dir.x);
                    animator.SetInteger("Y Input", (int)dir.y);

                    if (dir.x != 0)
                    {
                        if (dir.x > 0)
                        {
                            if (dir.y > 0)
                            {
                                currentdir = direction.upright;
                            }
                            else if (dir.y < 0) { currentdir = direction.downright; }
                            else { currentdir = direction.right; }
                        }
                        else
                        {
                            if (dir.y > 0)
                            {
                                currentdir = direction.upleft;
                            }
                            else if (dir.y < 0) { currentdir = direction.downleft; }
                            else { currentdir = direction.left; }
                        }
                    }

                    if (dir.x == 0 && dir.y != 0)
                    {
                        if (dir.y > 0)
                        {
                            currentdir = direction.up;
                        }
                        else
                        {
                            currentdir = direction.down;
                        }
                    }

                    rb.AddForce(movescale * dir.normalized);
                }
            }
        }
    }
}
