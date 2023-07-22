using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _pauseMenu;

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Debug.Log("Pause game");
        Time.timeScale = 0;
        _resumeButton.SetActive(true);
        _pauseButton.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _resumeButton.SetActive(false);
        _pauseButton.SetActive(true);
        _pauseMenu.SetActive(false);
    }
    
    public void BackToMenu()
    {
        //Load scene
        SceneManager.LoadScene(0);
    }
    
    public void SaveAndBackToMenu() {
        //Save game
        SaveSystem.Instance.SaveGame();
        
        //Load scene
        BackToMenu();
    }
}
