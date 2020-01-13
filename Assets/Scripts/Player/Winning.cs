using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    public float playerStayTime = 2f;
    bool win = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            win = true;
            StartCoroutine(Example());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            win = false;
            StartCoroutine(Example());
        }
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(playerStayTime);
        if(win)
        {
            ScoreScript.Score++;
            Debug.Log("Score =" + ScoreScript.Score);
            UIScripts.Replay();
        }
    }
}
