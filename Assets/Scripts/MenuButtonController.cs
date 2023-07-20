using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _normal, _press;
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
    
    public void SaveAndBackToMenu()
    {
        //Save game
        ObjectPooling objectPooling = GameObject.Find("PlatformPooling").GetComponent<ObjectPooling>();
        objectPooling.SaveObjectPooling("data.dat");
        
        //Load scene
        BackToMenu();
    }

    public void LoadGame() {
        Time.timeScale = 1;
        var op = SceneManager.LoadSceneAsync("Scenes/GameScene");
        op.completed += (AsyncOperation obj) => {
            GameObject platformPooling = GameObject.Find("PlatformPooling");
            ObjectPooling objectPooling = platformPooling.GetComponent<ObjectPooling>();
            GroundSpawner groundSpawner = platformPooling.GetComponent<GroundSpawner>();
            groundSpawner.isContinueGame = true;
            objectPooling.LoadGame("data.dat");
        };
    }
}
