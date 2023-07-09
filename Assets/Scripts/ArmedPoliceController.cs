using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedPoliceController : MonoBehaviour
{
    public GameObject bulletPrefab;
    float fireElapsedTime = 0;
    public float fireDelay = 3f;

    void FixedUpdate () {
        if (gameObject.transform.position.x < -20) {
            gameObject.SetActive(false);
        }
        
        fireElapsedTime += Time.fixedDeltaTime;
        if (fireElapsedTime >= fireDelay) {
            fireElapsedTime = 0;
            Fire();
        }
    }
    
    void Fire()
    {
        //Instantiate bullet
        Vector2 bulletPos = transform.position;
        bulletPos.x -= 0.2f;
        GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
        bullet.transform.parent = transform.parent;
        //Add velocity to bullet
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Dead();
        }
    }
}
