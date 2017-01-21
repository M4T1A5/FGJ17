using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform LastHit { get; set; }

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            // Player has fallen out of level for sure
            Destroy(gameObject);
        }
    }
}
