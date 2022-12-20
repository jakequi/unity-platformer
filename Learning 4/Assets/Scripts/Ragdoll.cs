using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float lowerBoundaryX;
    [SerializeField] float upperBoundaryX;
    [SerializeField] float lowerBoundaryY;
    [SerializeField] float upperBoundaryY;
    PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        transform.localScale = new Vector2 (playerController.deathScale, 1f);
        rb2d.velocity = new Vector2(Random.Range(lowerBoundaryX, upperBoundaryX) * playerController.deathScale, Random.Range(lowerBoundaryY, upperBoundaryY));
    }
}
