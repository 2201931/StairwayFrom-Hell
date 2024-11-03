using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public GameObject dialogBox;     // Reference to the UI dialog box
    public Text dialogText;          // Reference to the Text component in the dialog box

    private string[] sentences;
    private int currentSentenceIndex = 0;
    private bool isDialogActive = false;

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
        if (isDialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed, showing next sentence...");
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
            dialogText.text = sentences[currentSentenceIndex];
            Debug.Log("Displaying sentence: " + sentences[currentSentenceIndex]);
            currentSentenceIndex++;
        }
        else
        {
            Debug.Log("End of dialog reached.");
            EndDialog();
        }
    }

    public void EndDialog()
    {
        Debug.Log("Ending dialog.");
        dialogBox.SetActive(false);
        isDialogActive = false;
    }
}
