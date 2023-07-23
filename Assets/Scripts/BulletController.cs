using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    private Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        if (!IsVisibleFromCamera()) {
            Destroy(gameObject);
        }
    }

    bool IsVisibleFromCamera() {
        Vector3 viewportPosition =
            mainCamera.WorldToViewportPoint(transform
                .position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 &&
               viewportPosition.y <= 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            var _player = GameObject.Find("Player").GetComponent<PlayerController>();
            var slowDown = _player.SlowDown(true, 0.2f, 0.1f);
            StartCoroutine(slowDown);
            Destroy(gameObject);
        }
    }
}