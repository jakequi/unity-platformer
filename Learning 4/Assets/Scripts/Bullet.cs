using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerController player;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] float xSpeed;

    void Start(){
        player = FindObjectOfType<PlayerController>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        transform.localScale = new Vector2(-player.transform.localScale.x, 1f);
    }

    void Update()
    {
        rb2d.velocity = new Vector2(xSpeed, 0f);
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy") {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }

}
