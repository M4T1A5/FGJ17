using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private Transform[] spawnPoints;
    private readonly List<int> usedSpawns = new List<int>();

    private void Start()
    {
        GameManager.Instance.PlayerSpawner = this;

        spawnPoints = transform.GetComponentsInChildren<Transform>().Skip(1).ToArray();

        var playerCount = GameManager.Instance.PlayerCount;

        for (int i = 0; i < playerCount; i++)
        {
            SpawnPlayer(i, FindSpawn(i));
        }
    }

    private Vector3 FindSpawn(int playerId)
    {
        var foundSpawn = false;
        var spawnPoint = new Vector3();
        while (!foundSpawn)
        {
            var spawn = Random.Range(0, spawnPoints.Length);
            if (usedSpawns.Contains(spawn))
                continue;

            usedSpawns.Add(spawn);
            foundSpawn = true;
            spawnPoint = spawnPoints[spawn].position;
        }

        return spawnPoint;
    }

    private void SpawnPlayer(int playerId, Vector3 position)
    {
        var player = Instantiate(PlayerPrefab, position, Quaternion.identity) as GameObject;
        player.GetComponent<Player>().PlayerId = playerId;
        var renderer = player.GetComponentInChildren<Renderer>();

        switch (playerId)
        {
            case 0:
                renderer.material.color  = Color.blue;
                break;
            case 1:
                renderer.material.color = Color.red;
                break;
            case 2:
                renderer.material.color = Color.green;
                break;
            case 3:
                renderer.material.color = Color.yellow;
                break;
        }
    }

    public void RequestRespawn(int playerId)
    {
        var selectedSpawn = usedSpawns[playerId];
        SpawnPlayer(playerId, spawnPoints[selectedSpawn].position);
    }
}
