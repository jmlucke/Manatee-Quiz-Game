using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    // Start is called before the first frame update
    [TextArea(2, 6)] [SerializeField] string question = "Enter new quesetion her";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctInt;
    public string getQuestion()
        {
        return question;
        }
    public int getCorrecttAnswerIndex()
    {
        return correctInt;
    }
    public string getAnswer(int index)
    {
        return answers[index];
    }

}
