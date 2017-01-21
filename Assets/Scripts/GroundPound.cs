using UnityEngine;
using System.Collections;

public class GroundPound : MonoBehaviour
{
    public float Radius = 5;
    public float Power = 5;

    private bool hasJumped;

    private void Start()
    {
        var playerMovement = GetComponent<PlayerMovement>();
        playerMovement.PlayerJumpEvent += JumpEvent;
    }

    private void JumpEvent()
    {
        hasJumped = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Ground"))
        {
            if (hasJumped)
                hasJumped = false;
            return;
        }
        if (!hasJumped)
            return;

        hasJumped = false;

        var hits = Physics.OverlapSphere(transform.position, Radius,
            LayerMask.GetMask("Player"));
        var explosionPosition = transform.position;
        foreach (var hit in hits)
        {
            if (hit.transform != transform)
            {
                hit.GetComponent<Rigidbody>()
                    .AddExplosionForce(Power, explosionPosition, Radius, 0, ForceMode.Impulse);
                print(hit.transform.name);
            }
        }
    }
}
