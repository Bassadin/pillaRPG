using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]

public class Wander : MonoBehaviour
{
    //Public
    public float pursuitSpeed;
    public float wanderSpeed;
    public float directionChangeInterval;
    public bool followPlayer;

    //Internal
    float currentSpeed;
    Coroutine MoveCoroutine;
    Rigidbody2D rigidbody2D;
    Animator animator;
    Transform targetTransform = null;
    Vector3 endPosition;
    float currentAngle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());
    }

    public IEnumerator WanderRoutine()
    {
        while(true)
        {
            ChooseNewEndpoint();

            if (MoveCoroutine != null)
            {
                StopCoroutine(MoveCoroutine);
            }

            MoveCoroutine = StartCoroutine(Move(rigidbody2D, currentSpeed));

            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void ChooseNewEndpoint()
    {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentSpeed, 360);
        endPosition += Vector3FromAngle(currentAngle);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rigidbodyToMove != null)
            {
                animator.SetBool("isWalking", true);
                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endPosition, speed * Time.deltaTime);
                rigidbody2D.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }

        animator.SetBool("isWalking", false);
    }
}
