using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _normal, _press;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private SaveSystem _saveSystem;

    public void StartNewGame()
    {
        //Load scene
        SceneManager.LoadScene("Scenes/GameScene");
        Debug.Log("Load new game");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _normal;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _press;
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
