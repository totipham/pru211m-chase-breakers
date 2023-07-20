using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour {
    private const string PoolingSaveFileName = "data.dat";
    private GameObject _player;
    private GameObject _chasingPolice;
    private ObjectPooling _objectPooling;
    public static SaveSystem Instance;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start() {
        _player = GameObject.FindWithTag("Player");
        _chasingPolice = GameObject.FindWithTag("Police");
        _objectPooling = GameObject.Find("PlatformPooling").GetComponent<ObjectPooling>();
    }

    public void SaveGame() {
        SaveScore();
        SaveObjectPooling(PoolingSaveFileName);
        SaveChasingPolicePosition();
        SavePlayerPosition();
    }

    public void LoadGameFromSave() {
        //Load Object Pooling
        _objectPooling.LoadGame(PoolingSaveFileName);

        //Load score
        _player.GetComponent<PlayerController>().distance = PlayerPrefs.GetFloat("Score");

        //Load player position

        //Load chasing police position
    }

    private void SaveScore() {
        float distance = _player.GetComponent<PlayerController>().distance;
        PlayerPrefs.SetFloat("Score", distance);
    }

    private void SaveObjectPooling(string fileName) {
        _objectPooling.SaveObjectPooling(fileName);
    }

    private void SaveChasingPolicePosition() {
    }

    private void SavePlayerPosition() {
    }

    private void LoadChasingPolicePosition() {
    }

    private void LoadPlayerPosition() {
    }
}