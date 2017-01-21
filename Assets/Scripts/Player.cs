using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int PlayerId;
    public int LastHitTime = 5;
    public bool hasLastHit;
    public Transform LastHit;
    private scoreSystem _scoreSys;

    private Coroutine timer;

    private void Start()
    {
        _scoreSys = GameManager.Instance.ScoreSystem;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
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
    public void PlayerDeath()
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

        Destroy(gameObject);
    }


    /// <summary>
    /// if killer's LastHit is killed one (this GameObject), reset killer's LastHit 
    /// </summary>
    private void InformDeathToKiller()
    {
        if (LastHit == null)
            return;

        var killerHit = LastHit.GetComponent<Player>().LastHit;

        if (killerHit == transform)
        {
            LastHit.GetComponent<Player>().LastHit = null;
            hasLastHit = false;
        }

    }
}
