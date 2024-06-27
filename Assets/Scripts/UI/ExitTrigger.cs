using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public QuestionUI questionUI;
    public string question = "What is 2 + 2?";
    public string answer = "4";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            questionUI.ShowQuestion(question, answer);
        }
    }
}
