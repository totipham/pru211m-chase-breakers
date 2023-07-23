using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    public bool isGamePaused;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
