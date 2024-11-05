using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int jumpMode; //work damn you

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
}
