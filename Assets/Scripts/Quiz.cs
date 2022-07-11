using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions=new List<QuestionSO>();

     
    [SerializeField]  TextMeshProUGUI QuestionText;
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    //set to true for intital launch
    bool hasAnswerEarly=true;
    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKepp scoreKeeper;
    [Header("Progress")]
    [SerializeField] Slider progressBar;
    public bool isComplete;
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper= FindObjectOfType<ScoreKepp>();
        progressBar.maxValue = questions.Count;
        Debug.Log("1Awake");
        progressBar.value = 0;
        GetNextQuestion();
        Debug.Log("2Awake");
        DisplayQuestion();
       
        Debug.Log("Awake");
    }
     void Update()
    {
        
   
        timerImage.fillAmount = 0;
        if(timer.loadNextQuestion)
        {
            Debug.Log("1U");
            hasAnswerEarly = false;
             GetNextQuestion();
            //causes issue
            timer.loadNextQuestion=false;
        }else if(!hasAnswerEarly && !timer.isAnswerQuestion)
        {
            Debug.Log("2U");
            //to trigger method without risking an answer
            DisplayAnswer(-1);
            SetButtonState(false);
        }

    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.getCorrecttAnswerIndex())
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            QuestionText.text = "Manatee ho!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
            Debug.Log("Answer Display");
             
        }
        else
        {
            QuestionText.text = "Bad lama!";
            correctAnswerIndex = currentQuestion.getCorrecttAnswerIndex();
            string correctAnswer = currentQuestion.getAnswer(correctAnswerIndex);
            QuestionText.text = "Bad lama! Answer is \n " + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
             
        }

           


    }
   
        public void OnAnswerSelected(int index)
    {
        hasAnswerEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalcScore() + " %";
        Debug.Log("Answer Select");
        new WaitForSecondsRealtime(6);
        GetNextQuestion();

    }
    void GetNextQuestion()
    {
        if(questions.Count >0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuesiton();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsDons();
            Debug.Log("1Get Q");
        }
        Debug.Log("Get Q faile");
    }

    void GetRandomQuesiton()
    {
        int index = Random.Range(0,questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
         

    }

    void DisplayQuestion()
    {
         
        QuestionText.text = currentQuestion.getQuestion();



        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.getAnswer(i);
        }
        Debug.Log("DQ");
    }
    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {

            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
             

        }
    }

}
