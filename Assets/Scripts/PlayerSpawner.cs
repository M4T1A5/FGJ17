using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XInputDotNetPure;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private Transform[] spawnPoints;
    private readonly List<int> usedSpawns = new List<int>();

    private void Start()
    {
        spawnPoints = transform.GetComponentsInChildren<Transform>().Skip(1).ToArray();

        var playerCount = GameManager.Instance.PlayerCount;

        for (int i = 0; i < playerCount; i++)
        {
            var foundSpawn = false;
            do
            {
                var spawn = Random.Range(0, spawnPoints.Length);
                if(usedSpawns.Contains(spawn))
                    continue;

                usedSpawns.Add(spawn);
                var player = Instantiate(PlayerPrefab, spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                player.GetComponent<PlayerMovement>().PlayerIndex = (PlayerIndex) i;
                foundSpawn = true;
            } while (!foundSpawn);
        }
    }

    public void RequestRespawn(Transform player, int playerId)
    {
        player.position = spawnPoints[usedSpawns[playerId]].position;
        player.rotation = Quaternion.identity;
    }
}
