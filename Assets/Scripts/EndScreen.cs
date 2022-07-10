using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalscoreText;
    ScoreKepp scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKepp>();
    }
 

    public void ShowFinalScore()
    {
        finalscoreText.text = "Congrats aquatic enthusiast!\n Your score was" +
                                scoreKeeper.CalcScore() + "%";
    }
}
