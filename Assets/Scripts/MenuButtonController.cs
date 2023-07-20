using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
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
}
