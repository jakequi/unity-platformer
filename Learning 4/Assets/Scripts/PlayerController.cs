using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] PhysicsMaterial2D deadMaterial;
    [SerializeField] PolygonCollider2D polygonCollider2D;
    [SerializeField] BoxCollider2D boxCollider2d;
    [SerializeField] Animator animator;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject deadBody;
    [SerializeField] Transform gun;
    [SerializeField] Transform deathPosition;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpHeight = 5;
    [SerializeField] float climbSpeed = 5;
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float jumpBufferTime = 0.2f;
    
    float gravityScaleAtStart;
    float coyoteTimer;
    float jumpBufferTimer;
    bool isGrounded;
    bool isAlive = true;
    
    public float deathScale;


    void Start()
    {
        gravityScaleAtStart = rb2d.gravityScale;
    }

    void Update()
    {
        if(!isAlive){return;}
        Move();
        Shoot();
        Jump();
        ClimbLadder();
        Die();
    }

    void Move(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(horizontal * moveSpeed, rb2d.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2 (Mathf.Sign(rb2d.velocity.x), 1f);
        }
    }

    void Shoot(){
        if(Input.GetButtonDown("Fire1")){
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }

     void Jump() {
        isGrounded = boxCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if(isGrounded){
            coyoteTimer = coyoteTime;
        }
        else {
            coyoteTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")){
            jumpBufferTimer = jumpBufferTime;
        }
        else{ 
            jumpBufferTimer -= Time.deltaTime;
        }

        if (coyoteTimer > 0f && jumpBufferTimer > 0f) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
            jumpBufferTimer = 0f;
        }

        if(Input.GetButtonUp("Jump") && rb2d.velocity.y > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            coyoteTimer = 0f;
        }
    }

    void ClimbLadder(){
        float vertical = Input.GetAxisRaw("Vertical");
        if(boxCollider2d.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            animator.SetBool("isClimbing", true);
            rb2d.gravityScale = 0;
            rb2d.velocity = new Vector2(rb2d.velocity.x, vertical * climbSpeed);
            bool playerHasVerticalSpeed = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;
            animator.SetBool("isClimbing", playerHasVerticalSpeed);
        }
        else{
            rb2d.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
        }
    }

    void Die(){
        if(polygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards", "Water"))){
            // polygonCollider2D.sharedMaterial = deadMaterial;
            deathScale = transform.localScale.x;
            isAlive = false;
            animator.SetTrigger("isDead");
            if(polygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))){
                // rb2d.velocity = new Vector2(transform.localScale.x *-7.5f, 7.5f);
                Destroy(gameObject);
                Instantiate(deadBody, deathPosition.position, deathPosition.rotation);
            }
        }
    }

    public bool GetAlive(){
        return isAlive;
    }
}

/*     public void OnJump(InputAction.CallbackContext context) {
        if (context.performed){
            jumpBufferTimer = jumpBufferTime;
        }

        if(context.canceled && rb2d.velocity.y > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            coyoteTimer = 0f;
        }
    }

    void JumpCheck(){
        isGrounded = boxcollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if(isGrounded){
            coyoteTimer = coyoteTime;
        }
        else {
            coyoteTimer -= Time.deltaTime;
        }

        if (jumpBufferTimer > 0f && coyoteTimer > 0f){
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
            jumpBufferTimer = 0f;
        }
        jumpBufferTimer -= Time.deltaTime;
    } */