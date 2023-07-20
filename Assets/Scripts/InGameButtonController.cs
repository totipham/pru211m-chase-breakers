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
        //Load scene
        SceneManager.LoadScene("Scenes/GameScene");
        Debug.Log("Load new game");
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
        Debug.Log("Resume game");
        Time.timeScale = 1;
        _resumeButton.SetActive(false);
        _pauseButton.SetActive(true);
        _pauseMenu.SetActive(false);
    }
    
    public void BackToMenu()
    {
        //Load scene
        SceneManager.LoadScene("Scenes/Menu");
    }
    
    public void SaveAndBackToMenu() {
        // _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        // //Save score
        // PlayerPrefs.SetFloat("Score", _playerController.distance);
        //
        // //Save object pool game
        // ObjectPooling objectPooling = GameObject.Find("PlatformPooling").GetComponent<ObjectPooling>();
        // objectPooling.SaveObjectPooling("data.dat");
        
        //Save game
        SaveSystem.Instance.SaveGame();
        
        //Load scene
        BackToMenu();
    }
}
