using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    public Text CurrentScore;
    // Start is called before the first frame update
    void Start()
    {
        CurrentScore.text = "Score : " + ScoreScript.Score.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
