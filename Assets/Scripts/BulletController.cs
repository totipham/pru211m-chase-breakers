using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!IsVisibleFromCamera())
        {
            Destroy(gameObject);
        }
    }


    bool IsVisibleFromCamera()
    {
        Vector3 viewportPosition =
            mainCamera.WorldToViewportPoint(transform
                .position); //Chuyển vị trí của viên đạn thành tọa độ nằm trong main camera
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 &&
               viewportPosition.y <= 1; //Khoảng trong camera là từ 0 đến 1
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
