using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundPound : MonoBehaviour
{
    public float Radius = 5;
    public float Power = 5;

    public bool SuperHit;
    public float SuperHitRadius = 15;
    public float SuperHitPower = 15;

    public GameObject FatWave;

    private bool hasJumped;

    private void Start()
    {
        var playerMovement = GetComponent<PlayerMovement>();
        playerMovement.PlayerJumpEvent += JumpEvent;
    }

    public void JumpEvent()
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

        var radius = SuperHit ? SuperHitRadius : Radius;

        var hits = Physics.OverlapSphere(transform.position, radius,
            LayerMask.GetMask("Player"));
        var explosionPosition = transform.position;
        var hitPlayers = new List<Transform>();

        foreach (var hit in hits)
        {
            var root = Utility.GetRootObject(hit);

            if (root.transform != transform)
            {
                if(hitPlayers.Contains(root))
                    continue;

                hitPlayers.Add(root);

                if (SuperHit)
                {
                    root.GetComponent<Rigidbody>()
                        .AddExplosionForce(SuperHitPower, explosionPosition, SuperHitRadius,
                        0, ForceMode.Impulse);
                }
                else
                {
                    root.GetComponent<Rigidbody>()
                        .AddExplosionForce(Power, explosionPosition, Radius,
                        0, ForceMode.Impulse);
                }

                root.GetComponent<Player>().LastHit = transform;
            }
        }

        var effect = Instantiate(FatWave, transform.position, Quaternion.identity) as GameObject;
        if (SuperHit)
        {
            effect.transform.localScale = new Vector3(3, 3, 3);
            SuperHit = false;
        }
        Destroy(effect, 2.0f);
    }
}
