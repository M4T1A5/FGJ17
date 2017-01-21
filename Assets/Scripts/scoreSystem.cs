using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreSystem : MonoBehaviour
{

    public int players;
    public int[] Scores;
    public GameObject[] playerObjects;
    public Text[] Texts;
    private bool[] scoreActivation;



    public void ActivateScores(int playersInGame)
    {
        players = playersInGame;
        scoreActivation = new bool[players];

        for (int i = 0; i < playersInGame; i++)
        {
            scoreActivation[i] = true;

        }

        for (int j = 0; j < scoreActivation.Length; j++)
        {
            if (scoreActivation[j] == true)
            {
                playerObjects[j].SetActive(true);
            }
            else
            {
                playerObjects[j].SetActive(false);

            }

        }

    }



    

    /// <summary>
    /// call this to change player scores
    /// </summary>
    /// <param name="playerNumber"> player's ID to </param>
    /// <param name="gainedScore"> score you want to add to current score</param>
    public void updateScore(int playerNumber, int gainedScore)
    {
        Scores[playerNumber] = Scores[playerNumber] + gainedScore;
        Texts[playerNumber].text = Scores[playerNumber].ToString();
    }

    /// <summary>
    /// reset all scores to 0
    /// </summary>
    public void resetScores()
    {
        for (int k = 0; k < Scores.Length; k++)
        {
            Scores[k] = 0;
            Texts[k].text = Scores[k].ToString();

        }
    }

}
