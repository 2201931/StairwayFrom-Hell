using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private int jumpMode; //work damn you

    public Button quitButton; // Assign your Quit button in the Inspector

    void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitToKevin);
        }
        else
        {
            Debug.LogError("Quit Button is not assigned in the Inspector");
        }
    }

    public void SetEasyMode()
    {
        jumpMode = 0;
        PlayerPrefs.SetInt("JumpMode", jumpMode);
    }

    public void SetHardMode()
    {
        jumpMode = 1;
        PlayerPrefs.SetInt("JumpMode", jumpMode);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private void QuitToKevin()
    {
        Debug.Log("Quitting to Kevin scene...");
        SceneManager.LoadScene("Kevin");
    }
}
