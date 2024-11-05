using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SetEasyMode()
    {
        PlayerPrefs.SetInt("JumpMode", 0);
        PlayerPrefs.Save(); // Ensure the value is saved
    }

    public void SetHardMode()
    {
        PlayerPrefs.SetInt("JumpMode", 1);
        PlayerPrefs.Save(); // Ensure the value is saved
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
