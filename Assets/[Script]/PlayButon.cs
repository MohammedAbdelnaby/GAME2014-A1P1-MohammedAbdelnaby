using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButon : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main 1");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void Instruction()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
