using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _normal, _press;
    [SerializeField] private SaveSystem _saveSystem;
    public void StartNewGame() {
        //Load scene
        SceneManager.LoadScene("Scenes/GameScene");
        Time.timeScale = 1;
    }

    public void OnPointerUp(PointerEventData eventData) {
       _img.sprite = _normal; 
    }

    public void OnPointerDown(PointerEventData eventData) {
       _img.sprite = _press; 
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
        _saveSystem.SaveGame();
        
        //Load scene
        BackToMenu();
    }

    public void LoadGame() {
        Time.timeScale = 1;
        var op = SceneManager.LoadSceneAsync("Scenes/GameScene");
        op.completed += (AsyncOperation obj) => {
            _saveSystem.LoadGameFromSave();
            
            // GameObject platformPooling = GameObject.Find("PlatformPooling");
            // ObjectPooling objectPooling = platformPooling.GetComponent<ObjectPooling>();
            // GroundSpawner groundSpawner = platformPooling.GetComponent<GroundSpawner>();
            // _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            // _playerController.distance = PlayerPrefs.GetFloat("Score");
            // groundSpawner.isContinueGame = true;
            // objectPooling.LoadGame("data.dat");
        };
    }
}
