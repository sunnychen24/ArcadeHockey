using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokecheckController : MonoBehaviour
{
    private float lifetime = 0.833f;

    // Start is called before the first frame update
    void Start()
    {
        
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
