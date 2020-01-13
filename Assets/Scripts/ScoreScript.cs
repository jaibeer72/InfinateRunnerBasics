using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static int Score = 1;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Score");
        Score = 1;
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }
}
