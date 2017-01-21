using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int PlayerCount;

    public PlayerSpawner PlayerSpawner;

    public scoreSystem ScoreSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);

        ScoreSystem = FindObjectOfType<scoreSystem>();
    }

    public void SetPlayerCount(int players)
    {
        PlayerCount = players;
    }
}
