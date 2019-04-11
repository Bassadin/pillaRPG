using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movementVector = new Vector2();
    Animator animator;
    string animationState = "AnimationState";
    Rigidbody2D rb2D;

    enum charStates
    {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,

        idleSouth = 5
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        updateState();
    }
    void FixedUpdate()
    {
        moveCharacter();
    }

    private void moveCharacter()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        movementVector.Normalize();
        rb2D.velocity = movementVector * movementSpeed;
    }

    private void updateState()
    {
        if (movementVector.x > 0)
        {
            animator.SetInteger(animationState, (int)charStates.walkEast);
        }
        else if (movementVector.x < 0)
        {
            animator.SetInteger(animationState, (int)charStates.walkWest);
        }
        else if (movementVector.y > 0)
        {
            animator.SetInteger(animationState, (int)charStates.walkNorth);
        }
        else if (movementVector.y < 0)
        {
            animator.SetInteger(animationState, (int)charStates.walkSouth);
        }
        else
        {
            animator.SetInteger(animationState, (int)charStates.idleSouth);
        }
    }
}