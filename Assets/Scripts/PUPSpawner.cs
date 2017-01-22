using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PUPSpawner : MonoBehaviour
{
    //public List<GameObject> PowerUpPrefabs;
    public GameObject TrampolinePrefab;
    public float TrampolineFirstSpawnTimer = 60.0f;
    public float TrampolineSpawnInterval = 15.0f;

    [Tooltip("Put 0 for no limits.")]
    public int TrampolineAmountsPerGame = 1;
    private int m_amountOfTrampolinesSpawned = 0;

    private Transform[] spawnPoints;

    private float m_timer = 0.0f;

    private void Start()
    {
        spawnPoints = transform.GetComponentsInChildren<Transform>().Skip(1).ToArray();
    }

    private void Update()
    {
        m_timer += Time.deltaTime;

        if(m_amountOfTrampolinesSpawned == 0 && m_timer >= TrampolineFirstSpawnTimer)
        {
            m_amountOfTrampolinesSpawned++;
            SpawnPowerUp(TrampolinePrefab);
            m_timer = 0.0f;
        }
        else if (m_timer >= TrampolineSpawnInterval && m_amountOfTrampolinesSpawned <= TrampolineAmountsPerGame)
        {
            m_amountOfTrampolinesSpawned++;
            SpawnPowerUp(TrampolinePrefab);
            m_timer = 0.0f;
        }
    }

    private void SpawnPowerUp(GameObject pup)
    {
        var position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        var _pup = Instantiate(pup, position, Quaternion.identity) as GameObject;

        if(_pup == null)
        {
            Debug.LogError("Couldn't spawn power up!");
            return;
        }

        
    }
}
