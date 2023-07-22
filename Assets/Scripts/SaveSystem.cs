using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour {
    private GameObject _player;
    private GameObject _chasingPolice;
    private ObjectPooling _objectPooling;
    public static SaveSystem Instance;
    private String saveCharacterPath;
    private String savePoolingPath;
    
    [Serializable]
    private class CharacterData {
        public String characterName;
        public bool isVelocity;
        public SerializableVector characterPosition;
        public CharacterData(String characterName, SerializableVector characterPosition) {
            this.characterName = characterName;
            this.characterPosition = characterPosition;
            isVelocity = false;
        }
        
        public CharacterData(String characterName, SerializableVector characterPosition, bool isVelocity) {
            this.characterName = characterName;
            this.characterPosition = characterPosition;
            this.isVelocity = isVelocity;
        }
    }
    
    private class CharacterDataList {
        public List<CharacterData> characterDataList;
    }

    
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
        saveCharacterPath = Path.Combine(Application.persistentDataPath, "dataCharacter.dat");
        savePoolingPath = Path.Combine(Application.persistentDataPath, "dataPooling.dat");
        SaveScore();
        SaveObjectPooling(savePoolingPath);
        SaveCharacter(saveCharacterPath);
    }

    public void LoadGameFromSave() {
        saveCharacterPath = Path.Combine(Application.persistentDataPath, "dataCharacter.dat");
        savePoolingPath = Path.Combine(Application.persistentDataPath, "dataPooling.dat");
        _objectPooling = GameObject.Find("PlatformPooling").GetComponent<ObjectPooling>();
        _player = GameObject.FindWithTag("Player");
        
        //Load Object Pooling
        _objectPooling.LoadGame(savePoolingPath);

        //Load score
        _player.GetComponent<PlayerController>().distance = PlayerPrefs.GetFloat("Score");
        
        //Load character
        LoadCharacter(saveCharacterPath);

    }

    private void SaveScore() {
        float distance = _player.GetComponent<PlayerController>().distance;
        PlayerPrefs.SetFloat("Score", distance);
    }

    private void SaveObjectPooling(string fileName) {
        _objectPooling.SaveObjectPooling(fileName);
    }

    private void SaveCharacter(String fileSave) {
        CharacterDataList saveData = new CharacterDataList();
        saveData.characterDataList = new List<CharacterData>();
        
        SerializableVector playerPosition = _player.transform.position;
        SerializableVector chasingPolicePosition = _chasingPolice.transform.position;
        SerializableVector playerVelocity = (Vector3) _player.GetComponent<PlayerController>().velocity;
        
        CharacterData playerData = new CharacterData("Player", playerPosition);
        CharacterData playerVelocityData = new CharacterData("Player", playerVelocity, true);
        CharacterData chasingPoliceData = new CharacterData("ChasingPolice", chasingPolicePosition);
        
        saveData.characterDataList.Add(playerData);
        saveData.characterDataList.Add(chasingPoliceData);
        saveData.characterDataList.Add(playerVelocityData);
        
        string json = JsonUtility.ToJson(saveData, true);
        System.IO.File.WriteAllText(fileSave, json);
    }


    private void LoadCharacter(String fileSave) {
        _player = GameObject.FindWithTag("Player");
        _chasingPolice = GameObject.FindWithTag("Police");
        
        if (!System.IO.File.Exists(fileSave)) {
            return;
        }
        
        string json = System.IO.File.ReadAllText(fileSave);
        CharacterDataList savedData = JsonUtility.FromJson<CharacterDataList>(json);
        
        foreach (CharacterData data in savedData.characterDataList) {
            if (data.characterName == "Player") {
                if (data.isVelocity) {
                    _player.GetComponent<PlayerController>().velocity = (Vector3) data.characterPosition;
                } else {
                    _player.transform.position = data.characterPosition;
                }
            } else if (data.characterName == "ChasingPolice") {
                _chasingPolice.transform.position = data.characterPosition;
            }
        }
    }
}