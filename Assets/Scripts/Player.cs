using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform LastHit { get; set; }

    private void Start()
    {

    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            // Player has fallen out of level for sure
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            LastHit = collision.transform;
    }

    private void OnDestroy()
    {
        // TODO: Add points to the player that killed us
        //var playerId = (int) LastHit.GetComponent<PlayerMovement>().PlayerIndex;
        //GameManager.Instance.ScoreSystem.updateScore(playerId, 1);
    }
}
