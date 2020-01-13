using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    int score; 
    public TextMeshPro textPro;
    // Start is called before the first frame update
    void Start()
    {
        textPro = GetComponent<TextMeshPro>();
        score = ScoreScript.Score;
        textPro.text = score.ToString();
    }
}
