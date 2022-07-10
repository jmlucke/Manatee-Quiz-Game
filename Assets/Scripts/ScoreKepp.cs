using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKepp : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsDone = 0;
     
    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }


    public int GetQuestionsDon()
    {
        return questionsDone;
    }

    public void IncrementQuestionsDons()
    {
        questionsDone++;
    }
    public int CalcScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsDone * 100);
    }
}
