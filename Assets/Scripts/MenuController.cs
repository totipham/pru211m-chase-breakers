using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button resumeButton;

    private void Start()
    {
        resumeButton.interactable = PlayerPrefs.HasKey("Score");
    }
}