using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timerValue;
   [SerializeField] float answerTime=30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    public bool isAnswerQuestion=false;
    public bool loadNextQuestion;
    public float fillFraction;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }


    void UpdateTimer()
    {



        timerValue -= Time.deltaTime;

        if(isAnswerQuestion)
        {
            

            if(timerValue>0)
            {
                fillFraction = timerValue / answerTime;
            }
            else
            {
                isAnswerQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }

        }
        else
        {
             

            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnswerQuestion = true;
                timerValue = answerTime;
                loadNextQuestion = true;
            }
        }

       
    }
}
