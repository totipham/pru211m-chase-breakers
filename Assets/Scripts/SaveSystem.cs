using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem 
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _chasingPolice;
    [SerializeField] private ObjectPooling _objectPooling;
    
    public void SaveGame() {
        SaveScore();
        SaveObjectPooling();
        SaveChasingPolicePosition();
        SavePlayerPosition();
    }
    
    public void LoadGameFromSave() {
        //Load Object Pooling
        _objectPooling.LoadGame("data.dat");
        
        //Load score
        _player.GetComponent<PlayerController>().distance = PlayerPrefs.GetFloat("Score");
        
        //Load player position
        
        //Load chasing police position
        
    }

    private void SaveScore() {
        float distance = _player.GetComponent<PlayerController>().distance;        
        PlayerPrefs.SetFloat("Score", distance);
    }

    private void SaveObjectPooling() {
        _objectPooling.SaveObjectPooling("data.dat");
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
