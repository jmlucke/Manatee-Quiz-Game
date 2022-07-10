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
        progressBar.value = 0;
        GetNextQuestion();
           DisplayQuestion();

    }
    private void Update()
    {
        timerImage.fillAmount = 0;
        if(timer.loadNextQuestion)
        {
            hasAnswerEarly = false;
             GetNextQuestion();
            //causes issue
            timer.loadNextQuestion=false;
        }else if(!hasAnswerEarly && !timer.isAnswerQuestion)
        {
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
        }
         
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
