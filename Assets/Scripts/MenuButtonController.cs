using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {

    public void StartNewGame() {
        //Delete save
        PlayerPrefs.DeleteAll();
        
        //Load scene
        SceneManager.LoadScene("Scenes/GameScene");
        Time.timeScale = 1;
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
                SaveSystem.Instance.LoadGameFromSave();
            }
            
            // GameObject platformPooling = GameObject.Find("PlatformPooling");
            // GroundSpawner groundSpawner = platformPooling.GetComponent<GroundSpawner>();
            // groundSpawner.isContinueGame = true;
            // SaveSystem.Instance.LoadGameFromSave();
        };
    }
}