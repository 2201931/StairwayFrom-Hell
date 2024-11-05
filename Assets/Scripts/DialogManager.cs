using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public GameObject dialogBox;     // Reference to the UI dialog box
    public Text dialogText;          // Reference to the Text component in the dialog box
    public float typingSpeed = 0.05f; // Speed of typing effect in seconds

    private string[] sentences;
    private int currentSentenceIndex = 0;
    private bool isDialogActive = false;
    private bool isTyping = false;   // Track if text is still typing

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Use Space key to continue dialog
        if (isDialogActive && !isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialog(Dialog dialog)
    {
        Debug.Log("Dialog started with NPC.");
        dialogBox.SetActive(true);
        sentences = dialog.sentences;
        currentSentenceIndex = 0;
        isDialogActive = true;
        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (currentSentenceIndex < sentences.Length)
        {
            // Start typing the current sentence
            StopAllCoroutines();  // Stop any ongoing typing coroutine before starting a new one
            StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
            currentSentenceIndex++;
        }
        else
        {
            Debug.Log("End of dialog reached.");
            EndDialog();
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = ""; // Clear existing text
        isTyping = true;

        // Type each character one by one
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false; // Typing complete, ready to move to the next sentence
    }

    public void EndDialog()
    {
        Debug.Log("Ending dialog.");
        dialogBox.SetActive(false);
        isDialogActive = false;
    }
}

