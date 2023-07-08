using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button button = root.Q<Button>("ButtonStart");
        button.clicked += () => {
            Debug.Log("Button clicked");
        };
    }
}
