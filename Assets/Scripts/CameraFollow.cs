using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Rigidbody2D rb;
    private Transform puck;
    private FixedJoint2D player;
    public List<Transform> players = new List<Transform>();
    public List<GameObject> playerIndicators = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        puck = GameObject.Find("Puck").transform;
        player = GameObject.Find("Puck").GetComponent<FixedJoint2D>();
    }

    private void FixedUpdate()
    {
        if (player.connectedBody == null)
        {
            rb.MovePosition(new Vector2(puck.position.x, puck.position.y));
        }
        else
        {
            rb.MovePosition(player.connectedBody.position);
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Vector3 target = players[i].position;
            Vector3 screenPos = GetComponent<Camera>().WorldToScreenPoint(target);

            if (!Mathf.Approximately(screenPos.z, 0))
            {
                Vector3 halfScreen = new Vector3(Screen.width, Screen.height) / 2;
                Vector3 screenPosXY = new Vector3(screenPos.x, screenPos.y, 0);
                Vector3 center = screenPosXY - halfScreen;

                if (screenPos.z < 0)
                {
                    center *= -1;
                }

                if (screenPos.z < 0 || screenPos.x > Screen.width || screenPos.x < 0 || screenPos.y > Screen.height || screenPos.y < 0)
                {
                    playerIndicators[i].SetActive(true);
                    playerIndicators[i].transform.rotation = Quaternion.FromToRotation(Vector3.up, center);

                    Vector3 centerNorm = center.normalized;

                    if (centerNorm.x == 0)
                    {
                        centerNorm.x = 0.01f;
                    }

                    if (centerNorm.y == 0)
                    {
                        centerNorm.y = 0.01f;
                    }

                    Vector3 xPos = centerNorm * (halfScreen.x / Mathf.Abs(centerNorm.x));
                    Vector3 yPos = centerNorm * (halfScreen.y / Mathf.Abs(centerNorm.y));

                    if (xPos.sqrMagnitude < yPos.sqrMagnitude)
                    {
                        screenPos = halfScreen + xPos;
                    }
                    else
                    {
                        screenPos = halfScreen + yPos;
                    }
                }
                else
                {
                    playerIndicators[i].SetActive(false);
                }

                float margin = 70;
                screenPos.z = 0;
                screenPos.x = Mathf.Clamp(screenPos.x, margin, Screen.width - margin);
                screenPos.y = Mathf.Clamp(screenPos.y, margin, Screen.height - margin);

                playerIndicators[i].transform.position = screenPos;
            }
        }
    }
}
