using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine.UI;
using System.Collections;

public class QuestionUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject questionPanel;
    public TMP_Text questionText;
    public TMP_InputField answerInput;
    public PlayerHealth playerHealth;
    private PlayerControlls playerControlls;

    private string correctAnswer;

    private void Awake()
    {
        playerControlls = new PlayerControlls();
        // Attempt to find GameObject with "QuestionPanel" tag
        questionPanel = GameObject.Find("Question Panel");

        if (questionPanel == null)
        {
            Debug.LogError("QuestionPanel not found! Make sure it exists in the scene and has the correct tag.");
        }
        else
        {
            questionPanel.SetActive(false); // Ensure panel starts deactivated if found
        }
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component not found on the assigned Player GameObject. Make sure the PlayerHealth script is attached.");
            }
            else
            {
                Debug.Log("PlayerHealth component successfully assigned.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned in the Inspector.");
        }
    }

    void Start()
    {
        playerControlls.UIsubmit.Submit.performed += _ => OnSubmitAnswer();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
    }

    private void OnDisable()
    {
        playerControlls?.Disable();
    }

    public void ShowQuestion(string question, string answer)
    {
        // Check if questionPanel is valid
        if (questionPanel != null)
        {
            questionPanel.SetActive(true);
            questionText.text = question;
            correctAnswer = answer;
            answerInput.text = "";
            Time.timeScale = 0; // Pause the game

            if (answerInput != null)
            {
                answerInput.ActivateInputField();
            }
        }
        else
        {
            Debug.LogWarning("QuestionPanel reference is null. Cannot show question.");
            // Optionally handle this situation (e.g., show a default UI element or message)
        }
    }

    public void OnSubmitAnswer()
    {
        // Check if questionPanel is valid
        if (questionPanel != null)
        {
            Time.timeScale = 1; // Resume the game
            questionPanel.SetActive(false);

            if (answerInput.text.Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("Correct answer!");
                gameObject.SetActive(false);
                // Perform any action for correct answer, if needed
            }
            else
            {
                Debug.Log("Incorrect answer!");
                playerHealth.TakeDamage(1); // Reduce player health on incorrect answer
            }

            // Optionally destroy the GameObject here if needed
            // Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("QuestionPanel reference is null. Cannot submit answer.");
            // Optionally handle this situation (e.g., show a default UI element or message)
        }
    }
}
