using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour
{
    public float Power = 50;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var pound = Utility.GetRootObject(other).GetComponent<GroundPound>();
        if(pound == null)
            return;

        pound.SuperHit = true;
        pound.JumpEvent();

        var rb = pound.GetComponent<Rigidbody>();
        rb.velocity = new Vector3();
        rb.angularVelocity = new Vector3();
        rb.AddForce(0, Power, 0, ForceMode.Impulse);

        animator.SetTrigger("Play");

        AudioManager.Instance.PlaySoundEffect(GetComponent<AudioSource>());
        GameManager.Instance.PUPSpawner.m_trampolinesInWorld = 0;

        Destroy(gameObject, 3.5f);
    }
}
