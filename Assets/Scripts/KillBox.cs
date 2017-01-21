using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Collider referes to the child object the collider is part of
        // not the parent gameobject
        var parent = other.transform;
        while (parent.parent)
        {
            parent = parent.parent;
        }

        var player = parent.GetComponent<Player>();
        if (player != null)
        {
            player.PlayerDeath();
            GameManager.Instance.PlayerSpawner.
                RequestRespawn(player.PlayerId);
        }
    }
}
