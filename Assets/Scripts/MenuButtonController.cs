using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {

    public void StartNewGame() {
        //Delete save
        PlayerPrefs.DeleteAll();
        
        //Load scene
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        var op = SceneManager.LoadSceneAsync("Scenes/GameScene");

        op.completed += (AsyncOperation obj) =>
        {
            if (obj.isDone) {
                GameObject platformPooling = GameObject.Find("PlatformPooling");
                GroundSpawner groundSpawner = platformPooling.GetComponent<GroundSpawner>();
                groundSpawner.isContinueGame = true;
                SaveSystem.Instance = GameObject.FindWithTag("MainCamera").GetComponent<SaveSystem>();
                SaveSystem.Instance.LoadGameFromSave();
            }
            
            // GameObject platformPooling = GameObject.Find("PlatformPooling");
            // GroundSpawner groundSpawner = platformPooling.GetComponent<GroundSpawner>();
            // groundSpawner.isContinueGame = true;
            // SaveSystem.Instance.LoadGameFromSave();
        };
    }
}