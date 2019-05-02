using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movementVector = new Vector2();
    Animator animator;
    Rigidbody2D rb2D;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        UpdateState();
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        movementVector.Normalize();
        rb2D.velocity = movementVector * movementSpeed;
    }

    private void UpdateState()
    {
        if (Mathf.Approximately(movementVector.x, 0) && Mathf.Approximately(movementVector.y, 0)) {
            animator.SetBool("isWalking", false);
        }
        else {
            animator.SetFloat("xDir", movementVector.x);
            animator.SetFloat("yDir", movementVector.y);
        }
    }
}