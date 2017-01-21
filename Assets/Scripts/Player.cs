using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int PlayerId;
    public int LastHitTime;
    public bool hasLastHit;
    public Vector3 RespawnPoint;
    public Transform LastHit { get; set; }
    private scoreSystem _scoreSys;

    private Coroutine timer;

    private void Start()
    {
        _scoreSys = GameManager.Instance.ScoreSystem;
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            // Player has fallen out of level for sure               
            PlayerDeath();
            print("player has died");
                
        }
    }

   
    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if (timer != null)
            {
                StopCoroutine(timer);
            }
            
            LastHit = collision.transform;
            hasLastHit = true;
            timer = StartCoroutine(LastHitOff());
        }

    }




    //timer for LasHit duration
    IEnumerator LastHitOff()
    {
        yield return new WaitForSeconds(LastHitTime);
        LastHit = null;
        hasLastHit = false;
    }


    /// <summary>
    /// what happens when player dies
    /// </summary>

    private void PlayerDeath()
    {
        if (LastHit != null)
        {
            var lastHitId = LastHit.GetComponent<Player>().PlayerId;
            _scoreSys.updateScore(lastHitId, 1);
            
            InformDeathToKiller();
            LastHit = null;

        }
        else
        {
            _scoreSys.updateScore(PlayerId, -1);
        }

        gameObject.transform.position = RespawnPoint;
    }


    /// <summary>
    /// if killer's LastHit is killed one (this GameObject), reset killer's LastHit 
    /// </summary>


    private void InformDeathToKiller()
    {
        if(LastHit == null)
            return;
        
        Transform KillerHit = LastHit.GetComponent<Player>().LastHit;
        print(KillerHit.name + " this is killerhit");

        if (KillerHit == transform)
        {
            print("killer nuulled");
            LastHit.GetComponent<Player>().LastHit = null;
            hasLastHit = false;
        }

    }


    

   
}
