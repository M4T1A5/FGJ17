using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = Utility.GetRootObject(other).GetComponent<Player>();
        if (player != null)
        {
            player.PlayerDeath();
            GameManager.Instance.PlayerSpawner.
                RequestRespawn(player.PlayerId);
        }
    }
}
