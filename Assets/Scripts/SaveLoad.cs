using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _normal, _press;
    
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
}
