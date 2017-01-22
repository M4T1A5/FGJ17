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
    }

    private void SpawnPowerUp(Vector3 position, GameObject pup)
    {
        var _pup = Instantiate(pup, position, Quaternion.identity) as GameObject;

        if(_pup == null)
        {
            Debug.LogError("Couldn't spawn power up!");
            return;
        }

        if (_pup == TrampolinePrefab)
            m_amountOfTrampolinesSpawned++;
    }
}
