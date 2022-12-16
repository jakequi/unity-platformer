using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] BoxCollider2D flipCollider;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag != "Player"){
            moveSpeed = -moveSpeed;
            transform.localScale = new Vector2 (-(Mathf.Sign(rb2d.velocity.x)), 1f);
        }
    }
}
