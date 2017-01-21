using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiButtonScripts : MonoBehaviour
{
    public scoreSystem scores;
    public GameObject tittleMenu;
    

    public void playerButton(int numberOfPlayers)
    {
        scores.ActivateScores(numberOfPlayers);
        tittleMenu.SetActive(false);
    }


}
