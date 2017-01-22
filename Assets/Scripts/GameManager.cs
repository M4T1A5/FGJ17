using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int PlayerCount;

    public PlayerSpawner PlayerSpawner;

    public PUPSpawner PUPSpawner;

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
    }

    public void SetPlayerCount(int players)
    {
        PlayerCount = players;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
