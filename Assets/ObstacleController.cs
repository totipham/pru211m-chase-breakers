using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleController : MonoBehaviour {
    public float slowDownVelocity = 5f;
    public float slowDownTime = 0.5f;
    private PlayerController _player;
    
    // Start is called before the first frame update
    void OnEnable()
    {
       _player = GameObject.Find("Player").GetComponent<PlayerController>(); 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10f;
            var slowDown = _player.SlowDown(true, slowDownVelocity, slowDownTime);
            StartCoroutine(slowDown);
        } 
    }
}
