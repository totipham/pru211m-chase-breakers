using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickableFunction : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _normal, _press;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _resumeButton;
    
    public void StartNewGame() {
        //Load scene
        SceneManager.LoadScene("Scenes/GameScene");
        Debug.Log("Load new game");
    }

    public void OnPointerUp(PointerEventData eventData) {
       _img.sprite = _normal; 
    }

    public void OnPointerDown(PointerEventData eventData) {
       _img.sprite = _press; 
    }
    
    public void PauseGame() {
        Debug.Log("Pause game");
        Time.timeScale = 0;
        _resumeButton.SetActive(true);
        _pauseButton.SetActive(false);
    }
    
    public void ResumeGame() {
        Debug.Log("Resume game");
        Time.timeScale = 1;
        _resumeButton.SetActive(false);
        _pauseButton.SetActive(true);
    }
}
